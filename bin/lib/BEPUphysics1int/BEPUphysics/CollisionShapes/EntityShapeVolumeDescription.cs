using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEPUutilities;
using Deterministic.FixedPoint;
using FixMath.NET;

namespace BEPUphysics.CollisionShapes
{
    public struct EntityShapeVolumeDescription
    {
        public Matrix3x3 VolumeDistribution;
        public fp Volume;
    }
}
