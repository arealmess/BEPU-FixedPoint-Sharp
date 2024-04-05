﻿using System;
using BEPUphysics.Constraints.TwoEntity.Joints;
using BEPUutilities;
using FixMath.NET;
using Deterministic.FixedPoint;

namespace BEPUphysics.Constraints.TwoEntity.JointLimits
{
    /// <summary>
    /// Superclass of constraints which have a limited area of free movement.
    /// </summary>
    public abstract class JointLimit : Joint
    {
        /// <summary>
        /// Minimum velocity necessary for a bounce to occur at a joint limit.
        /// </summary>
        protected fp bounceVelocityThreshold = F64.C1;

        /// <summary>
        /// Bounciness of this joint limit.  0 is completely inelastic; 1 is completely elastic.
        /// </summary>
        protected fp bounciness;

        protected bool isLimitActive;

        /// <summary>
        /// Small area that the constraint can be violated without applying position correction.  Helps avoid jitter.
        /// </summary>
        protected fp margin = (fp)0.005m;

        /// <summary>
        /// Gets or sets the minimum velocity necessary for a bounce to occur at a joint limit.
        /// </summary>
        public fp BounceVelocityThreshold
        {
            get { return bounceVelocityThreshold; }
            set { bounceVelocityThreshold = MathHelper.Max(F64.C0, value); }
        }

        /// <summary>
        /// Gets or sets the bounciness of this joint limit.  0 is completely inelastic; 1 is completely elastic.
        /// </summary>
        public fp Bounciness
        {
            get { return bounciness; }
            set { bounciness = MathHelper.Clamp(value, F64.C0, F64.C1); }
        }

        /// <summary>
        /// Gets whether or not the limit is currently exceeded.  While violated, the constraint will apply impulses in an attempt to stop further violation and to correct any current error.
        /// This is true whenever the limit is touched.
        /// </summary>
        public bool IsLimitExceeded
        {
            get { return isLimitActive; }
        }

        /// <summary>
        /// Gets or sets the small area that the constraint can be violated without applying position correction.  Helps avoid jitter.
        /// </summary>
        public fp Margin
        {
            get { return margin; }
            set { margin = MathHelper.Max(value, F64.C0); }
        }

        /// <summary>
        /// Computes the bounce velocity for this limit.
        /// </summary>
        /// <param name="impactVelocity">Velocity of the impact on the limit.</param>
        /// <returns>The resulting bounce velocity of the impact.</returns>
        protected fp ComputeBounceVelocity(fp impactVelocity)
        {
            var lowThreshold = bounceVelocityThreshold * F64.C0p3;
            var velocityFraction = MathHelper.Clamp((impactVelocity - lowThreshold) / (bounceVelocityThreshold - lowThreshold + Toolbox.Epsilon), F64.C0, F64.C1);
            return velocityFraction * impactVelocity * Bounciness;
        }

    }
}