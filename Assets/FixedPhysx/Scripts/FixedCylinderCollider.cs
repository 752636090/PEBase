using FixedMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixedPhysx
{
    public class FixedCylinderCollider : FixedColliderBase
    {
        public FixedFloat Radius;

        public FixedCylinderCollider(FixedColliderConfig config)
        {
            Position = config.Position;
            Radius = config.Radius;
            Name = config.Name;
        }

        /// <summary>
        /// 以后用八叉树
        /// </summary>
        public void DetectCollision(List<FixedColliderBase> colliders, ref FixedVector3 velocity, ref FixedVector3 borderAdjust)
        {
            if (velocity == FixedVector3.Zero)
            {
                return;
            }

            List<FixedCollisionInfo> collisionInfoLst = new List<FixedCollisionInfo>();
            FixedVector3 normal = FixedVector3.Zero;
            FixedVector3 adjust = FixedVector3.Zero;
            for (int i = 0; i < colliders.Count; i++)
            {
                if (DetectCollision(colliders[i], ref normal, ref adjust))
                {
                    FixedCollisionInfo info = new FixedCollisionInfo
                    {
                        Collider = colliders[i],
                        Normal = normal,
                        BorderAdjust = adjust
                    };
                    collisionInfoLst.Add(info);
                } 
            }

            if (collisionInfoLst.Count == 1) // 单个碰撞体，修正速度方向以及校正位置
            {
                FixedCollisionInfo info = collisionInfoLst[0];
                velocity = CorrectVelocity(velocity, info.Normal);
                borderAdjust = info.BorderAdjust;
                this.Log($"单个碰撞体，校正速度：{velocity.ConvertViewVector3()}");
            }
            else if (collisionInfoLst.Count > 1)
            {
                FixedVector3 centerNormal = FixedVector3.Zero;
                FixedCollisionInfo info = null;
                FixedArgs borderNormalAngle = CalcMaxNormalAngle(collisionInfoLst, velocity, ref centerNormal, ref info);
                FixedArgs angle = FixedVector3.Angle(-velocity, centerNormal);
                if (angle > borderNormalAngle)
                {
                    velocity = CorrectVelocity(velocity, info.Normal);
                    this.Log($"多个碰撞体，校正速度：{velocity.ConvertViewVector3()}");
                    FixedVector3 adjustSum = FixedVector3.Zero;
                    for (int i = 0; i < collisionInfoLst.Count; i++)
                    {
                        adjustSum += collisionInfoLst[i].BorderAdjust;
                    }
                    borderAdjust = adjustSum; 
                }
                else
                {
                    velocity = FixedVector3.Zero;
                    this.Log($"速度方向反向量在校正法线夹角内，无法移动：{angle.ConvertViewAngle()}");
                }
            }
            else
            {
                //this.Log("没有碰撞");
            }
        }

        private FixedArgs CalcMaxNormalAngle(List<FixedCollisionInfo> infoLst, FixedVector3 velocity, ref FixedVector3 centerNormal,
            ref FixedCollisionInfo info)
        {
            for (int i = 0; i < infoLst.Count; i++)
            {
                centerNormal += infoLst[i].Normal;
            }
            centerNormal /= infoLst.Count;

            FixedArgs normalAngle = FixedArgs.Zero;
            FixedArgs velocityAngle = FixedArgs.Zero;

            for (int i = 0; i < infoLst.Count; i++)
            {
                FixedArgs tmpNormalAngle = FixedVector3.Angle(centerNormal, infoLst[i].Normal);
                if (normalAngle < tmpNormalAngle)
                {
                    normalAngle = tmpNormalAngle;
                }

                // 找出速度方向与法线方向夹角最大的碰撞法线，速度校正由这个法线来决定
                FixedArgs tmpVelocityAngle = FixedVector3.Angle(velocity, infoLst[i].Normal);
                if (velocityAngle < tmpVelocityAngle)
                {
                    velocityAngle = tmpVelocityAngle;
                    info = infoLst[i];
                }
            }

            return normalAngle;
        }

        private FixedVector3 CorrectVelocity(FixedVector3 velocity, FixedVector3 normal)
        {
            if (normal == FixedVector3.Zero)
            {
                return velocity;
            }

            // 确保是靠近，不是远离
            if (FixedVector3.Angle(normal, velocity) > FixedArgs.HalfPi)
            {
                FixedFloat prjLen = FixedVector3.Dot(velocity, normal);
                if (prjLen != 0)
                {
                    velocity -= prjLen * normal;
                }
            }
            return velocity;
        }

        protected override bool DetectBoxCollision(FixedBoxCollider collider, ref FixedVector3 normal, ref FixedVector3 borderAdjust)
        {
            FixedVector3 disOffset = Position - collider.Position;
            // 计算向量在长方体轴向上的投影长度
            FixedFloat disDotX = FixedVector3.Dot(disOffset, collider.Rotation[0]);
            FixedFloat disDotZ = FixedVector3.Dot(disOffset, collider.Rotation[2]);

            // 投影钳制在x轴向里
            FixedFloat clampX = FixedCalc.Clamp(disDotX, -collider.Size.X, collider.Size.X);
            // 投影钳制在z轴向里
            FixedFloat clampZ = FixedCalc.Clamp(disDotZ, -collider.Size.Z, collider.Size.Z);

            // 计算轴向上的投影向量
            FixedVector3 projectionX = clampX * collider.Rotation[0];
            FixedVector3 projectionZ = clampZ * collider.Rotation[2];

            // 计算表面最近的接触点：碰撞体中心位置+轴向偏移
            FixedVector3 point = collider.Position;
            point += projectionX + projectionZ;

            FixedVector3 p2o = Position - point;
            p2o.Y = 0;

            if (FixedVector3.SqrMagnitube(p2o) > Radius * Radius)
            {
                return false;
            }
            else
            {
                normal = p2o.Normalized;
                FixedFloat len = p2o.Magnitude;
                borderAdjust = normal * (Radius - len);
                return true;
            }
        }

        protected override bool DetectSphereCollision(FixedCylinderCollider collider, ref FixedVector3 normal, ref FixedVector3 borderAdjust)
        {
            FixedVector3 disOffset = Position - collider.Position;
            if (FixedVector3.SqrMagnitube(disOffset) > (Radius + collider.Radius) * (Radius + collider.Radius))
            {
                return false;
            }
            else
            {
                normal = disOffset.Normalized;
                borderAdjust = normal * (Radius + collider.Radius - disOffset.Magnitude);
                return true;
            }
        }
    }
}
