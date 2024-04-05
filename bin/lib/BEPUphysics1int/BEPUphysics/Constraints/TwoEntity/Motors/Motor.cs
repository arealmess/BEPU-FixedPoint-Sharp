using Deterministic.FixedPoint;
using FixMath.NET;

namespace BEPUphysics.Constraints.TwoEntity.Motors
{
    /// <summary>
    /// Superclass of constraints which do work and change the velocity of connected entities, but have no specific position target.
    /// </summary>
    public abstract class Motor : TwoEntityConstraint
    {
        protected fp maxForceDt = Fix64.MaxValue;
        protected fp maxForceDtSquared = Fix64.MaxValue;

        /// <summary>
        /// Softness divided by the timestep to maintain timestep independence.
        /// </summary>
        internal fp usedSoftness;

        /// <summary>
        /// Computes the maxForceDt and maxForceDtSquared fields.
        /// </summary>
        protected void ComputeMaxForces(fp maxForce, fp dt)
        {
            //Determine maximum force
            if (maxForce < Fix64.MaxValue)
            {
                maxForceDt = maxForce * dt;
                maxForceDtSquared = maxForceDt * maxForceDt;
            }
            else
            {
                maxForceDt = Fix64.MaxValue;
                maxForceDtSquared = Fix64.MaxValue;
            }
        }
    }
}