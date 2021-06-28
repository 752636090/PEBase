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

            FixedVector3 normal = FixedVector3.Zero;
            FixedVector3 adjust = FixedVector3.Zero;
            if (DetectCollision(colliders[0], ref normal, ref adjust))
            {
                this.Log("碰撞.");
            }
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
            FixedVector3 projectionZ = clampX * collider.Rotation[2];

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

        }
    }
}
