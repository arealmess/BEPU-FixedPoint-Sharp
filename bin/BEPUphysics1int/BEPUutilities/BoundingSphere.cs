using FixMath.NET; 
using Deterministic.FixedPoint;

namespace BEPUutilities
{    
    /// <summary>
    /// Provides XNA-like bounding sphere functionality.
    /// </summary>
    public struct BoundingSphere
    {
        /// <summary>
        /// Radius of the sphere.
        /// </summary>
        public fp Radius;
        /// <summary>
        /// Location of the center of the sphere.
        /// </summary>
        public Vector3 Center;

        /// <summary>
        /// Constructs a new bounding sphere.
        /// </summary>
        /// <param name="center">Location of the center of the sphere.</param>
        /// <param name="radius">Radius of the sphere.</param>
        public BoundingSphere(Vector3 center, fp radius)
        {
            this.Center = center;
            this.Radius = radius;
        }
    }
}
