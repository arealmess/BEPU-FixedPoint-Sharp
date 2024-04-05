using System;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;

using BEPUutilities;
using FixMath.NET;
using Deterministic.FixedPoint;

namespace BEPUphysics.CollisionShapes.ConvexShapes
{
    ///<summary>
    /// Symmetrical shape with a circular base and a point at the top.
    ///</summary>
    public class ConeShape : ConvexShape
    {

        fp height;
        ///<summary>
        /// Gets or sets the height of the cone.
        ///</summary>
        public fp Height
        {
            get { return height; }
            set
            {
                height = value;
                OnShapeChanged();
            }
        }

        fp radius;
        ///<summary>
        /// Gets or sets the radius of the cone base.
        ///</summary>
        public fp Radius
        {
            get { return radius; }
            set
            {
                radius = value;
                OnShapeChanged();
            }
        }

        ///<summary>
        /// Constructs a new cone shape.
        ///</summary>
        ///<param name="height">Height of the cone.</param>
        ///<param name="radius">Radius of the cone base.</param>
        public ConeShape(fp height, fp radius)
        {
            this.height = height;
            this.radius = radius;

            UpdateConvexShapeInfo(ComputeDescription(height, radius, collisionMargin));
        }

        ///<summary>
        /// Constructs a new cone shape.
        ///</summary>
        ///<param name="height">Height of the cone.</param>
        ///<param name="radius">Radius of the cone base.</param>
        /// <param name="description">Cached information about the shape. Assumed to be correct; no extra processing or validation is performed.</param>
        public ConeShape(fp height, fp radius, ConvexShapeDescription description)
        {
            this.height = height;
            this.radius = radius;

            UpdateConvexShapeInfo(description);
        }


        protected override void OnShapeChanged()
        {
            UpdateConvexShapeInfo(ComputeDescription(height, radius, collisionMargin));
            base.OnShapeChanged();
        }


        /// <summary>
        /// Computes a convex shape description for a ConeShape.
        /// </summary>
        ///<param name="height">Height of the cone.</param>
        ///<param name="radius">Radius of the cone base.</param>
        ///<param name="collisionMargin">Collision margin of the shape.</param>
        /// <returns>Description required to define a convex shape.</returns>
        public static ConvexShapeDescription ComputeDescription(fp height, fp radius, fp collisionMargin)
        {
            ConvexShapeDescription description;
            description.EntityShapeVolume.Volume = F64.OneThird * MathHelper.Pi * radius * radius * height;

            description.EntityShapeVolume.VolumeDistribution = new Matrix3x3();
            fp diagValue = (F64.C0p1 * height * height + F64.C0p15 * radius * radius);
            description.EntityShapeVolume.VolumeDistribution.M11 = diagValue;
            description.EntityShapeVolume.VolumeDistribution.M22 = F64.C0p3 * radius * radius;
            description.EntityShapeVolume.VolumeDistribution.M33 = diagValue;

            description.MaximumRadius = collisionMargin + MathHelper.Max(F64.C0p75 * height, fixmath.Sqrt(F64.C0p0625 * height * height + radius * radius));

            fp denominator = radius / height;
            denominator = denominator / fixmath.Sqrt(denominator * denominator + F64.C1);
            description.MinimumRadius = collisionMargin + MathHelper.Min(fp._0_25 * height, denominator * F64.C0p75 * height);

            description.CollisionMargin = collisionMargin;
            return description;
        }


        ///<summary>
        /// Gets the extreme point of the shape in local space in a given direction.
        ///</summary>
        ///<param name="direction">Direction to find the extreme point in.</param>
        ///<param name="extremePoint">Extreme point on the shape.</param>
        public override void GetLocalExtremePointWithoutMargin(ref Vector3 direction, out Vector3 extremePoint)
        {
            //Is it the tip of the cone?
            fp sinThetaSquared = radius * radius / (radius * radius + height * height);
            //If d.Y * d.Y / d.LengthSquared >= sinthetaSquared
            if (direction.Y > F64.C0 && direction.Y * direction.Y >= direction.LengthSquared() * sinThetaSquared)
            {
                extremePoint = new Vector3(F64.C0, F64.C0p75 * height, F64.C0);
                return;
            }
            //Is it a bottom edge of the cone?
            fp horizontalLengthSquared = direction.X * direction.X + direction.Z * direction.Z;
            if (horizontalLengthSquared > Toolbox.Epsilon)
            {
                var radOverSigma = radius / fixmath.Sqrt(horizontalLengthSquared);
                extremePoint = new Vector3((radOverSigma * direction.X), F64.Cm0p25 * height, (radOverSigma * direction.Z));
            }
            else // It's pointing almost straight down...
                extremePoint = new Vector3(F64.C0, F64.Cm0p25 * height, F64.C0);


        }


        /// <summary>
        /// Retrieves an instance of an EntityCollidable that uses this EntityShape.  Mainly used by compound bodies.
        /// </summary>
        /// <returns>EntityCollidable that uses this shape.</returns>
        public override EntityCollidable GetCollidableInstance()
        {
            return new ConvexCollidable<ConeShape>(this);
        } 
    }
}
