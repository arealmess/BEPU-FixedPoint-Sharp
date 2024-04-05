using Deterministic.FixedPoint;
using FixMath.NET;

namespace BEPUphysics.Materials
{
    ///<summary>
    /// Contains the blended friction and bounciness of a pair of objects.
    ///</summary>
    public struct InteractionProperties
    {
        ///<summary>
        /// Kinetic friction between the pair of objects.
        ///</summary>
        public fp KineticFriction;
        ///<summary>
        /// Static friction between the pair of objects.
        ///</summary>
        public fp StaticFriction;
        ///<summary>
        /// Bounciness between the pair of objects.
        ///</summary>
        public fp Bounciness;
    }
}
