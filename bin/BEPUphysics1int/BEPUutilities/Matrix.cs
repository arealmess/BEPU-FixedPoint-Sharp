using FixMath.NET;
using System;
using Deterministic.FixedPoint;

namespace BEPUutilities
{
    /// <summary>
    /// Provides XNA-like 4x4 matrix math.
    /// </summary>
    public struct Matrix
    {
        /// <summary>
        /// Value at row 1, column 1 of the matrix.
        /// </summary>
        public fp M11;

        /// <summary>
        /// Value at row 1, column 2 of the matrix.
        /// </summary>
        public fp M12;

        /// <summary>
        /// Value at row 1, column 3 of the matrix.
        /// </summary>
        public fp M13;

        /// <summary>
        /// Value at row 1, column 4 of the matrix.
        /// </summary>
        public fp M14;

        /// <summary>
        /// Value at row 2, column 1 of the matrix.
        /// </summary>
        public fp M21;

        /// <summary>
        /// Value at row 2, column 2 of the matrix.
        /// </summary>
        public fp M22;

        /// <summary>
        /// Value at row 2, column 3 of the matrix.
        /// </summary>
        public fp M23;

        /// <summary>
        /// Value at row 2, column 4 of the matrix.
        /// </summary>
        public fp M24;

        /// <summary>
        /// Value at row 3, column 1 of the matrix.
        /// </summary>
        public fp M31;

        /// <summary>
        /// Value at row 3, column 2 of the matrix.
        /// </summary>
        public fp M32;

        /// <summary>
        /// Value at row 3, column 3 of the matrix.
        /// </summary>
        public fp M33;

        /// <summary>
        /// Value at row 3, column 4 of the matrix.
        /// </summary>
        public fp M34;

        /// <summary>
        /// Value at row 4, column 1 of the matrix.
        /// </summary>
        public fp M41;

        /// <summary>
        /// Value at row 4, column 2 of the matrix.
        /// </summary>
        public fp M42;

        /// <summary>
        /// Value at row 4, column 3 of the matrix.
        /// </summary>
        public fp M43;

        /// <summary>
        /// Value at row 4, column 4 of the matrix.
        /// </summary>
        public fp M44;

        /// <summary>
        /// Constructs a new 4 row, 4 column matrix.
        /// </summary>
        /// <param name="m11">Value at row 1, column 1 of the matrix.</param>
        /// <param name="m12">Value at row 1, column 2 of the matrix.</param>
        /// <param name="m13">Value at row 1, column 3 of the matrix.</param>
        /// <param name="m14">Value at row 1, column 4 of the matrix.</param>
        /// <param name="m21">Value at row 2, column 1 of the matrix.</param>
        /// <param name="m22">Value at row 2, column 2 of the matrix.</param>
        /// <param name="m23">Value at row 2, column 3 of the matrix.</param>
        /// <param name="m24">Value at row 2, column 4 of the matrix.</param>
        /// <param name="m31">Value at row 3, column 1 of the matrix.</param>
        /// <param name="m32">Value at row 3, column 2 of the matrix.</param>
        /// <param name="m33">Value at row 3, column 3 of the matrix.</param>
        /// <param name="m34">Value at row 3, column 4 of the matrix.</param>
        /// <param name="m41">Value at row 4, column 1 of the matrix.</param>
        /// <param name="m42">Value at row 4, column 2 of the matrix.</param>
        /// <param name="m43">Value at row 4, column 3 of the matrix.</param>
        /// <param name="m44">Value at row 4, column 4 of the matrix.</param>
        public Matrix(fp m11, fp m12, fp m13, fp m14,
                      fp m21, fp m22, fp m23, fp m24,
                      fp m31, fp m32, fp m33, fp m34,
                      fp m41, fp m42, fp m43, fp m44)
        {
            this.M11 = m11;
            this.M12 = m12;
            this.M13 = m13;
            this.M14 = m14;

            this.M21 = m21;
            this.M22 = m22;
            this.M23 = m23;
            this.M24 = m24;

            this.M31 = m31;
            this.M32 = m32;
            this.M33 = m33;
            this.M34 = m34;

            this.M41 = m41;
            this.M42 = m42;
            this.M43 = m43;
            this.M44 = m44;
        }

        /// <summary>
        /// Gets or sets the translation component of the transform.
        /// </summary>
        public Vector3 Translation
        {
            get
            {
                return new Vector3()
                {
                    X = M41,
                    Y = M42,
                    Z = M43
                };
            }
            set
            {
                M41 = value.X;
                M42 = value.Y;
                M43 = value.Z;
            }
        }

        /// <summary>
        /// Gets or sets the backward vector of the matrix.
        /// </summary>
        public Vector3 Backward
        {
            get
            {
#if !WINDOWS
                Vector3 vector = new Vector3();
#else
                Vector3 vector;
#endif
                vector.X = M31;
                vector.Y = M32;
                vector.Z = M33;
                return vector;
            }
            set
            {
                M31 = value.X;
                M32 = value.Y;
                M33 = value.Z;
            }
        }

        /// <summary>
        /// Gets or sets the down vector of the matrix.
        /// </summary>
        public Vector3 Down
        {
            get
            {
#if !WINDOWS
                Vector3 vector = new Vector3();
#else
                Vector3 vector;
#endif
                vector.X = -M21;
                vector.Y = -M22;
                vector.Z = -M23;
                return vector;
            }
            set
            {
                M21 = -value.X;
                M22 = -value.Y;
                M23 = -value.Z;
            }
        }

        /// <summary>
        /// Gets or sets the forward vector of the matrix.
        /// </summary>
        public Vector3 Forward
        {
            get
            {
#if !WINDOWS
                Vector3 vector = new Vector3();
#else
                Vector3 vector;
#endif
                vector.X = -M31;
                vector.Y = -M32;
                vector.Z = -M33;
                return vector;
            }
            set
            {
                M31 = -value.X;
                M32 = -value.Y;
                M33 = -value.Z;
            }
        }

        /// <summary>
        /// Gets or sets the left vector of the matrix.
        /// </summary>
        public Vector3 Left
        {
            get
            {
#if !WINDOWS
                Vector3 vector = new Vector3();
#else
                Vector3 vector;
#endif
                vector.X = -M11;
                vector.Y = -M12;
                vector.Z = -M13;
                return vector;
            }
            set
            {
                M11 = -value.X;
                M12 = -value.Y;
                M13 = -value.Z;
            }
        }

        /// <summary>
        /// Gets or sets the right vector of the matrix.
        /// </summary>
        public Vector3 Right
        {
            get
            {
#if !WINDOWS
                Vector3 vector = new Vector3();
#else
                Vector3 vector;
#endif
                vector.X = M11;
                vector.Y = M12;
                vector.Z = M13;
                return vector;
            }
            set
            {
                M11 = value.X;
                M12 = value.Y;
                M13 = value.Z;
            }
        }

        /// <summary>
        /// Gets or sets the up vector of the matrix.
        /// </summary>
        public Vector3 Up
        {
            get
            {
#if !WINDOWS
                Vector3 vector = new Vector3();
#else
                Vector3 vector;
#endif
                vector.X = M21;
                vector.Y = M22;
                vector.Z = M23;
                return vector;
            }
            set
            {
                M21 = value.X;
                M22 = value.Y;
                M23 = value.Z;
            }
        }


        /// <summary>
        /// Computes the determinant of the matrix.
        /// </summary>
        /// <returns></returns>
        public fp Determinant()
        {
            //Compute the re-used 2x2 determinants.
            fp det1 = M33 * M44 - M34 * M43;
            fp det2 = M32 * M44 - M34 * M42;
            fp det3 = M32 * M43 - M33 * M42;
            fp det4 = M31 * M44 - M34 * M41;
            fp det5 = M31 * M43 - M33 * M41;
            fp det6 = M31 * M42 - M32 * M41;
            return
                (M11 * ((M22 * det1 - M23 * det2) + M24 * det3)) -
                (M12 * ((M21 * det1 - M23 * det4) + M24 * det5)) +
                (M13 * ((M21 * det2 - M22 * det4) + M24 * det6)) -
                (M14 * ((M21 * det3 - M22 * det5) + M23 * det6));
        }

        /// <summary>
        /// Transposes the matrix in-place.
        /// </summary>
        public void Transpose()
        {
            fp intermediate = M12;
            M12 = M21;
            M21 = intermediate;

            intermediate = M13;
            M13 = M31;
            M31 = intermediate;

            intermediate = M14;
            M14 = M41;
            M41 = intermediate;

            intermediate = M23;
            M23 = M32;
            M32 = intermediate;

            intermediate = M24;
            M24 = M42;
            M42 = intermediate;

            intermediate = M34;
            M34 = M43;
            M43 = intermediate;
        }

        /// <summary>
        /// Creates a matrix representing the given axis and angle rotation.
        /// </summary>
        /// <param name="axis">Axis around which to rotate.</param>
        /// <param name="angle">Angle to rotate around the axis.</param>
        /// <returns>Matrix created from the axis and angle.</returns>
        public static Matrix CreateFromAxisAngle(Vector3 axis, fp angle)
        {
            Matrix toReturn;
            CreateFromAxisAngle(ref axis, angle, out toReturn);
            return toReturn;
        }

        /// <summary>
        /// Creates a matrix representing the given axis and angle rotation.
        /// </summary>
        /// <param name="axis">Axis around which to rotate.</param>
        /// <param name="angle">Angle to rotate around the axis.</param>
        /// <param name="result">Matrix created from the axis and angle.</param>
        public static void CreateFromAxisAngle(ref Vector3 axis, fp angle, out Matrix result)
        {
            fp xx = axis.X * axis.X;
            fp yy = axis.Y * axis.Y;
            fp zz = axis.Z * axis.Z;
            fp xy = axis.X * axis.Y;
            fp xz = axis.X * axis.Z;
            fp yz = axis.Y * axis.Z;

            fp sinAngle = fixmath.Sin(angle);
            fp oneMinusCosAngle = fp._1 - fixmath.Cos(angle);

            result.M11 = fp._1 + oneMinusCosAngle * (xx - fp._1);
            result.M21 = -axis.Z * sinAngle + oneMinusCosAngle * xy;
            result.M31 = axis.Y * sinAngle + oneMinusCosAngle * xz;
            result.M41 = fp._0;

            result.M12 = axis.Z * sinAngle + oneMinusCosAngle * xy;
            result.M22 = fp._1 + oneMinusCosAngle * (yy - fp._1);
            result.M32 = -axis.X * sinAngle + oneMinusCosAngle * yz;
            result.M42 = fp._0;

            result.M13 = -axis.Y * sinAngle + oneMinusCosAngle * xz;
            result.M23 = axis.X * sinAngle + oneMinusCosAngle * yz;
            result.M33 = fp._1 + oneMinusCosAngle * (zz - fp._1);
            result.M43 = fp._0;

            result.M14 = fp._0;
            result.M24 = fp._0;
            result.M34 = fp._0;
            result.M44 = fp._1;
        }

        /// <summary>
        /// Creates a rotation matrix from a quaternion.
        /// </summary>
        /// <param name="quaternion">Quaternion to convert.</param>
        /// <param name="result">Rotation matrix created from the quaternion.</param>
        public static void CreateFromQuaternion(ref Quaternion quaternion, out Matrix result)
        {
            fp qX2 = quaternion.X + quaternion.X;
            fp qY2 = quaternion.Y + quaternion.Y;
            fp qZ2 = quaternion.Z + quaternion.Z;
            fp XX = qX2 * quaternion.X;
            fp YY = qY2 * quaternion.Y;
            fp ZZ = qZ2 * quaternion.Z;
            fp XY = qX2 * quaternion.Y;
            fp XZ = qX2 * quaternion.Z;
            fp XW = qX2 * quaternion.W;
            fp YZ = qY2 * quaternion.Z;
            fp YW = qY2 * quaternion.W;
            fp ZW = qZ2 * quaternion.W;

            result.M11 = fp._1 - YY - ZZ;
            result.M21 = XY - ZW;
            result.M31 = XZ + YW;
            result.M41 = fp._0;

            result.M12 = XY + ZW;
            result.M22 = fp._1 - XX - ZZ;
            result.M32 = YZ - XW;
            result.M42 = fp._0;

            result.M13 = XZ - YW;
            result.M23 = YZ + XW;
            result.M33 = fp._1 - XX - YY;
            result.M43 = fp._0;

            result.M14 = fp._0;
            result.M24 = fp._0;
            result.M34 = fp._0;
            result.M44 = fp._1;
        }

        /// <summary>
        /// Creates a rotation matrix from a quaternion.
        /// </summary>
        /// <param name="quaternion">Quaternion to convert.</param>
        /// <returns>Rotation matrix created from the quaternion.</returns>
        public static Matrix CreateFromQuaternion(Quaternion quaternion)
        {
            Matrix toReturn;
            CreateFromQuaternion(ref quaternion, out toReturn);
            return toReturn;
        }

        /// <summary>
        /// Multiplies two matrices together.
        /// </summary>
        /// <param name="a">First matrix to multiply.</param>
        /// <param name="b">Second matrix to multiply.</param>
        /// <param name="result">Combined transformation.</param>
        public static void Multiply(ref Matrix a, ref Matrix b, out Matrix result)
        {
            fp resultM11 = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31 + a.M14 * b.M41;
            fp resultM12 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32 + a.M14 * b.M42;
            fp resultM13 = a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33 + a.M14 * b.M43;
            fp resultM14 = a.M11 * b.M14 + a.M12 * b.M24 + a.M13 * b.M34 + a.M14 * b.M44;

            fp resultM21 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31 + a.M24 * b.M41;
            fp resultM22 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32 + a.M24 * b.M42;
            fp resultM23 = a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33 + a.M24 * b.M43;
            fp resultM24 = a.M21 * b.M14 + a.M22 * b.M24 + a.M23 * b.M34 + a.M24 * b.M44;

            fp resultM31 = a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31 + a.M34 * b.M41;
            fp resultM32 = a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32 + a.M34 * b.M42;
            fp resultM33 = a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33 + a.M34 * b.M43;
            fp resultM34 = a.M31 * b.M14 + a.M32 * b.M24 + a.M33 * b.M34 + a.M34 * b.M44;

            fp resultM41 = a.M41 * b.M11 + a.M42 * b.M21 + a.M43 * b.M31 + a.M44 * b.M41;
            fp resultM42 = a.M41 * b.M12 + a.M42 * b.M22 + a.M43 * b.M32 + a.M44 * b.M42;
            fp resultM43 = a.M41 * b.M13 + a.M42 * b.M23 + a.M43 * b.M33 + a.M44 * b.M43;
            fp resultM44 = a.M41 * b.M14 + a.M42 * b.M24 + a.M43 * b.M34 + a.M44 * b.M44;

            result.M11 = resultM11;
            result.M12 = resultM12;
            result.M13 = resultM13;
            result.M14 = resultM14;

            result.M21 = resultM21;
            result.M22 = resultM22;
            result.M23 = resultM23;
            result.M24 = resultM24;

            result.M31 = resultM31;
            result.M32 = resultM32;
            result.M33 = resultM33;
            result.M34 = resultM34;

            result.M41 = resultM41;
            result.M42 = resultM42;
            result.M43 = resultM43;
            result.M44 = resultM44;
        }


        /// <summary>
        /// Multiplies two matrices together.
        /// </summary>
        /// <param name="a">First matrix to multiply.</param>
        /// <param name="b">Second matrix to multiply.</param>
        /// <returns>Combined transformation.</returns>
        public static Matrix Multiply(Matrix a, Matrix b)
        {
            Matrix result;
            Multiply(ref a, ref b, out result);
            return result;
        }


        /// <summary>
        /// Scales all components of the matrix.
        /// </summary>
        /// <param name="matrix">Matrix to scale.</param>
        /// <param name="scale">Amount to scale.</param>
        /// <param name="result">Scaled matrix.</param>
        public static void Multiply(ref Matrix matrix, fp scale, out Matrix result)
        {
            result.M11 = matrix.M11 * scale;
            result.M12 = matrix.M12 * scale;
            result.M13 = matrix.M13 * scale;
            result.M14 = matrix.M14 * scale;

            result.M21 = matrix.M21 * scale;
            result.M22 = matrix.M22 * scale;
            result.M23 = matrix.M23 * scale;
            result.M24 = matrix.M24 * scale;

            result.M31 = matrix.M31 * scale;
            result.M32 = matrix.M32 * scale;
            result.M33 = matrix.M33 * scale;
            result.M34 = matrix.M34 * scale;

            result.M41 = matrix.M41 * scale;
            result.M42 = matrix.M42 * scale;
            result.M43 = matrix.M43 * scale;
            result.M44 = matrix.M44 * scale;
        }

        /// <summary>
        /// Multiplies two matrices together.
        /// </summary>
        /// <param name="a">First matrix to multiply.</param>
        /// <param name="b">Second matrix to multiply.</param>
        /// <returns>Combined transformation.</returns>
        public static Matrix operator *(Matrix a, Matrix b)
        {
            Matrix toReturn;
            Multiply(ref a, ref b, out toReturn);
            return toReturn;
        }

        /// <summary>
        /// Scales all components of the matrix by the given value.
        /// </summary>
        /// <param name="m">First matrix to multiply.</param>
        /// <param name="f">Scaling value to apply to all components of the matrix.</param>
        /// <returns>Product of the multiplication.</returns>
        public static Matrix operator *(Matrix m, fp f)
        {
            Matrix result;
            Multiply(ref m, f, out result);
            return result;
        }

        /// <summary>
        /// Scales all components of the matrix by the given value.
        /// </summary>
        /// <param name="m">First matrix to multiply.</param>
        /// <param name="f">Scaling value to apply to all components of the matrix.</param>
        /// <returns>Product of the multiplication.</returns>
        public static Matrix operator *(fp f, Matrix m)
        {
            Matrix result;
            Multiply(ref m, f, out result);
            return result;
        }

        /// <summary>
        /// Transforms a vector using a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static void Transform(ref Vector4 v, ref Matrix matrix, out Vector4 result)
        {
            fp vX = v.X;
            fp vY = v.Y;
            fp vZ = v.Z;
            fp vW = v.W;
            result.X = vX * matrix.M11 + vY * matrix.M21 + vZ * matrix.M31 + vW * matrix.M41;
            result.Y = vX * matrix.M12 + vY * matrix.M22 + vZ * matrix.M32 + vW * matrix.M42;
            result.Z = vX * matrix.M13 + vY * matrix.M23 + vZ * matrix.M33 + vW * matrix.M43;
            result.W = vX * matrix.M14 + vY * matrix.M24 + vZ * matrix.M34 + vW * matrix.M44;
        }

        /// <summary>
        /// Transforms a vector using a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to apply to the vector.</param>
        /// <returns>Transformed vector.</returns>
        public static Vector4 Transform(Vector4 v, Matrix matrix)
        {
            Vector4 toReturn;
            Transform(ref v, ref matrix, out toReturn);
            return toReturn;
        }

        /// <summary>
        /// Transforms a vector using the transpose of a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to tranpose and apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static void TransformTranspose(ref Vector4 v, ref Matrix matrix, out Vector4 result)
        {
            fp vX = v.X;
            fp vY = v.Y;
            fp vZ = v.Z;
            fp vW = v.W;
            result.X = vX * matrix.M11 + vY * matrix.M12 + vZ * matrix.M13 + vW * matrix.M14;
            result.Y = vX * matrix.M21 + vY * matrix.M22 + vZ * matrix.M23 + vW * matrix.M24;
            result.Z = vX * matrix.M31 + vY * matrix.M32 + vZ * matrix.M33 + vW * matrix.M34;
            result.W = vX * matrix.M41 + vY * matrix.M42 + vZ * matrix.M43 + vW * matrix.M44;
        }

        /// <summary>
        /// Transforms a vector using the transpose of a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to tranpose and apply to the vector.</param>
        /// <returns>Transformed vector.</returns>
        public static Vector4 TransformTranspose(Vector4 v, Matrix matrix)
        {
            Vector4 toReturn;
            TransformTranspose(ref v, ref matrix, out toReturn);
            return toReturn;
        }

        /// <summary>
        /// Transforms a vector using a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static void Transform(ref Vector3 v, ref Matrix matrix, out Vector4 result)
        {
            result.X = v.X * matrix.M11 + v.Y * matrix.M21 + v.Z * matrix.M31 + matrix.M41;
            result.Y = v.X * matrix.M12 + v.Y * matrix.M22 + v.Z * matrix.M32 + matrix.M42;
            result.Z = v.X * matrix.M13 + v.Y * matrix.M23 + v.Z * matrix.M33 + matrix.M43;
            result.W = v.X * matrix.M14 + v.Y * matrix.M24 + v.Z * matrix.M34 + matrix.M44;
        }

        /// <summary>
        /// Transforms a vector using a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to apply to the vector.</param>
        /// <returns>Transformed vector.</returns>
        public static Vector4 Transform(Vector3 v, Matrix matrix)
        {
            Vector4 toReturn;
            Transform(ref v, ref matrix, out toReturn);
            return toReturn;
        }

        /// <summary>
        /// Transforms a vector using the transpose of a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to tranpose and apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static void TransformTranspose(ref Vector3 v, ref Matrix matrix, out Vector4 result)
        {
            result.X = v.X * matrix.M11 + v.Y * matrix.M12 + v.Z * matrix.M13 + matrix.M14;
            result.Y = v.X * matrix.M21 + v.Y * matrix.M22 + v.Z * matrix.M23 + matrix.M24;
            result.Z = v.X * matrix.M31 + v.Y * matrix.M32 + v.Z * matrix.M33 + matrix.M34;
            result.W = v.X * matrix.M41 + v.Y * matrix.M42 + v.Z * matrix.M43 + matrix.M44;
        }

        /// <summary>
        /// Transforms a vector using the transpose of a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to tranpose and apply to the vector.</param>
        /// <returns>Transformed vector.</returns>
        public static Vector4 TransformTranspose(Vector3 v, Matrix matrix)
        {
            Vector4 toReturn;
            TransformTranspose(ref v, ref matrix, out toReturn);
            return toReturn;
        }

        /// <summary>
        /// Transforms a vector using a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static void Transform(ref Vector3 v, ref Matrix matrix, out Vector3 result)
        {
            fp vX = v.X;
            fp vY = v.Y;
            fp vZ = v.Z;
            result.X = vX * matrix.M11 + vY * matrix.M21 + vZ * matrix.M31 + matrix.M41;
            result.Y = vX * matrix.M12 + vY * matrix.M22 + vZ * matrix.M32 + matrix.M42;
            result.Z = vX * matrix.M13 + vY * matrix.M23 + vZ * matrix.M33 + matrix.M43;
        }

        /// <summary>
        /// Transforms a vector using the transpose of a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to tranpose and apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static void TransformTranspose(ref Vector3 v, ref Matrix matrix, out Vector3 result)
        {
            fp vX = v.X;
            fp vY = v.Y;
            fp vZ = v.Z;
            result.X = vX * matrix.M11 + vY * matrix.M12 + vZ * matrix.M13 + matrix.M14;
            result.Y = vX * matrix.M21 + vY * matrix.M22 + vZ * matrix.M23 + matrix.M24;
            result.Z = vX * matrix.M31 + vY * matrix.M32 + vZ * matrix.M33 + matrix.M34;
        }

        /// <summary>
        /// Transforms a vector using a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static void TransformNormal(ref Vector3 v, ref Matrix matrix, out Vector3 result)
        {
            fp vX = v.X;
            fp vY = v.Y;
            fp vZ = v.Z;
            result.X = vX * matrix.M11 + vY * matrix.M21 + vZ * matrix.M31;
            result.Y = vX * matrix.M12 + vY * matrix.M22 + vZ * matrix.M32;
            result.Z = vX * matrix.M13 + vY * matrix.M23 + vZ * matrix.M33;
        }

        /// <summary>
        /// Transforms a vector using a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to apply to the vector.</param>
        /// <returns>Transformed vector.</returns>
        public static Vector3 TransformNormal(Vector3 v, Matrix matrix)
        {
            Vector3 toReturn;
            TransformNormal(ref v, ref matrix, out toReturn);
            return toReturn;
        }

        /// <summary>
        /// Transforms a vector using the transpose of a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to tranpose and apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static void TransformNormalTranspose(ref Vector3 v, ref Matrix matrix, out Vector3 result)
        {
            fp vX = v.X;
            fp vY = v.Y;
            fp vZ = v.Z;
            result.X = vX * matrix.M11 + vY * matrix.M12 + vZ * matrix.M13;
            result.Y = vX * matrix.M21 + vY * matrix.M22 + vZ * matrix.M23;
            result.Z = vX * matrix.M31 + vY * matrix.M32 + vZ * matrix.M33;
        }

        /// <summary>
        /// Transforms a vector using the transpose of a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to tranpose and apply to the vector.</param>
        /// <returns>Transformed vector.</returns>
        public static Vector3 TransformNormalTranspose(Vector3 v, Matrix matrix)
        {
            Vector3 toReturn;
            TransformNormalTranspose(ref v, ref matrix, out toReturn);
            return toReturn;
        }


        /// <summary>
        /// Transposes the matrix.
        /// </summary>
        /// <param name="m">Matrix to transpose.</param>
        /// <param name="transposed">Matrix to transpose.</param>
        public static void Transpose(ref Matrix m, out Matrix transposed)
        {
            fp intermediate = m.M12;
            transposed.M12 = m.M21;
            transposed.M21 = intermediate;

            intermediate = m.M13;
            transposed.M13 = m.M31;
            transposed.M31 = intermediate;

            intermediate = m.M14;
            transposed.M14 = m.M41;
            transposed.M41 = intermediate;

            intermediate = m.M23;
            transposed.M23 = m.M32;
            transposed.M32 = intermediate;

            intermediate = m.M24;
            transposed.M24 = m.M42;
            transposed.M42 = intermediate;

            intermediate = m.M34;
            transposed.M34 = m.M43;
            transposed.M43 = intermediate;

            transposed.M11 = m.M11;
            transposed.M22 = m.M22;
            transposed.M33 = m.M33;
            transposed.M44 = m.M44;
        }

        /// <summary>
        /// Inverts the matrix.
        /// </summary>
        /// <param name="m">Matrix to invert.</param>
        /// <param name="inverted">Inverted version of the matrix.</param>
        public static void Invert(ref Matrix m, out Matrix inverted)
        {
			Matrix4x8.Invert(ref m, out inverted);
        }

        /// <summary>
        /// Inverts the matrix.
        /// </summary>
        /// <param name="m">Matrix to invert.</param>
        /// <returns>Inverted version of the matrix.</returns>
        public static Matrix Invert(Matrix m)
        {
            Matrix inverted;
            Invert(ref m, out inverted);
            return inverted;
        }
		
        /// <summary>
        /// Inverts the matrix using a process that only works for rigid transforms.
        /// </summary>
        /// <param name="m">Matrix to invert.</param>
        /// <param name="inverted">Inverted version of the matrix.</param>
        public static void InvertRigid(ref Matrix m, out Matrix inverted)
        {
            //Invert (transpose) the upper left 3x3 rotation.
            fp intermediate = m.M12;
            inverted.M12 = m.M21;
            inverted.M21 = intermediate;

            intermediate = m.M13;
            inverted.M13 = m.M31;
            inverted.M31 = intermediate;

            intermediate = m.M23;
            inverted.M23 = m.M32;
            inverted.M32 = intermediate;

            inverted.M11 = m.M11;
            inverted.M22 = m.M22;
            inverted.M33 = m.M33;

            //Translation component
            var vX = m.M41;
            var vY = m.M42;
            var vZ = m.M43;
            inverted.M41 = -(vX * inverted.M11 + vY * inverted.M21 + vZ * inverted.M31);
            inverted.M42 = -(vX * inverted.M12 + vY * inverted.M22 + vZ * inverted.M32);
            inverted.M43 = -(vX * inverted.M13 + vY * inverted.M23 + vZ * inverted.M33);

            //Last chunk.
            inverted.M14 = fp._0;
            inverted.M24 = fp._0;
            inverted.M34 = fp._0;
            inverted.M44 = fp._1;
        }

        /// <summary>
        /// Inverts the matrix using a process that only works for rigid transforms.
        /// </summary>
        /// <param name="m">Matrix to invert.</param>
        /// <returns>Inverted version of the matrix.</returns>
        public static Matrix InvertRigid(Matrix m)
        {
            Matrix inverse;
            InvertRigid(ref m, out inverse);
            return inverse;
        }

        /// <summary>
        /// Gets the 4x4 identity matrix.
        /// </summary>
        public static Matrix Identity
        {
            get
            {
                Matrix toReturn;
                toReturn.M11 = fp._1;
                toReturn.M12 = fp._0;
                toReturn.M13 = fp._0;
                toReturn.M14 = fp._0;

                toReturn.M21 = fp._0;
                toReturn.M22 = fp._1;
                toReturn.M23 = fp._0;
                toReturn.M24 = fp._0;

                toReturn.M31 = fp._0;
                toReturn.M32 = fp._0;
                toReturn.M33 = fp._1;
                toReturn.M34 = fp._0;

                toReturn.M41 = fp._0;
                toReturn.M42 = fp._0;
                toReturn.M43 = fp._0;
                toReturn.M44 = fp._1;
                return toReturn;
            }
        }

        /// <summary>
        /// Creates a right handed orthographic projection.
        /// </summary>
        /// <param name="left">Leftmost coordinate of the projected area.</param>
        /// <param name="right">Rightmost coordinate of the projected area.</param>
        /// <param name="bottom">Bottom coordinate of the projected area.</param>
        /// <param name="top">Top coordinate of the projected area.</param>
        /// <param name="zNear">Near plane of the projection.</param>
        /// <param name="zFar">Far plane of the projection.</param>
        /// <param name="projection">The resulting orthographic projection matrix.</param>
        public static void CreateOrthographicRH(fp left, fp right, fp bottom, fp top, fp zNear, fp zFar, out Matrix projection)
        {
            fp width = right - left;
            fp height = top - bottom;
            fp depth = zFar - zNear;
            projection.M11 = F64.C2 / width;
            projection.M12 = fp._0;
            projection.M13 = fp._0;
            projection.M14 = fp._0;

            projection.M21 = fp._0;
            projection.M22 = F64.C2 / height;
            projection.M23 = fp._0;
            projection.M24 = fp._0;

            projection.M31 = fp._0;
            projection.M32 = fp._0;
            projection.M33 = -1 / depth;
            projection.M34 = fp._0;

            projection.M41 = (left + right) / -width;
            projection.M42 = (top + bottom) / -height;
            projection.M43 = zNear / -depth;
            projection.M44 = fp._1;

        }

        /// <summary>
        /// Creates a right-handed perspective matrix.
        /// </summary>
        /// <param name="fieldOfView">Field of view of the perspective in radians.</param>
        /// <param name="aspectRatio">Width of the viewport over the height of the viewport.</param>
        /// <param name="nearClip">Near clip plane of the perspective.</param>
        /// <param name="farClip">Far clip plane of the perspective.</param>
        /// <param name="perspective">Resulting perspective matrix.</param>
        public static void CreatePerspectiveFieldOfViewRH(fp fieldOfView, fp aspectRatio, fp nearClip, fp farClip, out Matrix perspective)
        {
            fp h = fp._1 / fixmath.Tan(fieldOfView / F64.C2);
            fp w = h / aspectRatio;
            perspective.M11 = w;
            perspective.M12 = fp._0;
            perspective.M13 = fp._0;
            perspective.M14 = fp._0;

            perspective.M21 = fp._0;
            perspective.M22 = h;
            perspective.M23 = fp._0;
            perspective.M24 = fp._0;

            perspective.M31 = fp._0;
            perspective.M32 = fp._0;
            perspective.M33 = farClip / (nearClip - farClip);
            perspective.M34 = -1;

            perspective.M41 = fp._0;
            perspective.M42 = fp._0;
            perspective.M44 = fp._0;
            perspective.M43 = nearClip * perspective.M33;

        }

        /// <summary>
        /// Creates a right-handed perspective matrix.
        /// </summary>
        /// <param name="fieldOfView">Field of view of the perspective in radians.</param>
        /// <param name="aspectRatio">Width of the viewport over the height of the viewport.</param>
        /// <param name="nearClip">Near clip plane of the perspective.</param>
        /// <param name="farClip">Far clip plane of the perspective.</param>
        /// <returns>Resulting perspective matrix.</returns>
        public static Matrix CreatePerspectiveFieldOfViewRH(fp fieldOfView, fp aspectRatio, fp nearClip, fp farClip)
        {
            Matrix perspective;
            CreatePerspectiveFieldOfViewRH(fieldOfView, aspectRatio, nearClip, farClip, out perspective);
            return perspective;
        }

        /// <summary>
        /// Creates a view matrix pointing from a position to a target with the given up vector.
        /// </summary>
        /// <param name="position">Position of the camera.</param>
        /// <param name="target">Target of the camera.</param>
        /// <param name="upVector">Up vector of the camera.</param>
        /// <param name="viewMatrix">Look at matrix.</param>
        public static void CreateLookAtRH(ref Vector3 position, ref Vector3 target, ref Vector3 upVector, out Matrix viewMatrix)
        {
            Vector3 forward;
            Vector3.Subtract(ref target, ref position, out forward);
            CreateViewRH(ref position, ref forward, ref upVector, out viewMatrix);
        }

        /// <summary>
        /// Creates a view matrix pointing from a position to a target with the given up vector.
        /// </summary>
        /// <param name="position">Position of the camera.</param>
        /// <param name="target">Target of the camera.</param>
        /// <param name="upVector">Up vector of the camera.</param>
        /// <returns>Look at matrix.</returns>
        public static Matrix CreateLookAtRH(Vector3 position, Vector3 target, Vector3 upVector)
        {
            Matrix lookAt;
            Vector3 forward;
            Vector3.Subtract(ref target, ref position, out forward);
            CreateViewRH(ref position, ref forward, ref upVector, out lookAt);
            return lookAt;
        }


        /// <summary>
        /// Creates a view matrix pointing in a direction with a given up vector.
        /// </summary>
        /// <param name="position">Position of the camera.</param>
        /// <param name="forward">Forward direction of the camera.</param>
        /// <param name="upVector">Up vector of the camera.</param>
        /// <param name="viewMatrix">Look at matrix.</param>
        public static void CreateViewRH(ref Vector3 position, ref Vector3 forward, ref Vector3 upVector, out Matrix viewMatrix)
        {
            Vector3 z;
            fp length = forward.Length();
            Vector3.Divide(ref forward, -length, out z);
            Vector3 x;
            Vector3.Cross(ref upVector, ref z, out x);
            x.Normalize();
            Vector3 y;
            Vector3.Cross(ref z, ref x, out y);

            viewMatrix.M11 = x.X;
            viewMatrix.M12 = y.X;
            viewMatrix.M13 = z.X;
            viewMatrix.M14 = fp._0;
            viewMatrix.M21 = x.Y;
            viewMatrix.M22 = y.Y;
            viewMatrix.M23 = z.Y;
            viewMatrix.M24 = fp._0;
            viewMatrix.M31 = x.Z;
            viewMatrix.M32 = y.Z;
            viewMatrix.M33 = z.Z;
            viewMatrix.M34 = fp._0;
            Vector3.Dot(ref x, ref position, out viewMatrix.M41);
            Vector3.Dot(ref y, ref position, out viewMatrix.M42);
            Vector3.Dot(ref z, ref position, out viewMatrix.M43);
            viewMatrix.M41 = -viewMatrix.M41;
            viewMatrix.M42 = -viewMatrix.M42;
            viewMatrix.M43 = -viewMatrix.M43;
            viewMatrix.M44 = fp._1;

        }

        /// <summary>
        /// Creates a view matrix pointing looking in a direction with a given up vector.
        /// </summary>
        /// <param name="position">Position of the camera.</param>
        /// <param name="forward">Forward direction of the camera.</param>
        /// <param name="upVector">Up vector of the camera.</param>
        /// <returns>Look at matrix.</returns>
        public static Matrix CreateViewRH(Vector3 position, Vector3 forward, Vector3 upVector)
        {
            Matrix lookat;
            CreateViewRH(ref position, ref forward, ref upVector, out lookat);
            return lookat;
        }



        /// <summary>
        /// Creates a world matrix pointing from a position to a target with the given up vector.
        /// </summary>
        /// <param name="position">Position of the transform.</param>
        /// <param name="forward">Forward direction of the transformation.</param>
        /// <param name="upVector">Up vector which is crossed against the forward vector to compute the transform's basis.</param>
        /// <param name="worldMatrix">World matrix.</param>
        public static void CreateWorldRH(ref Vector3 position, ref Vector3 forward, ref Vector3 upVector, out Matrix worldMatrix)
        {
            Vector3 z;
            fp length = forward.Length();
            Vector3.Divide(ref forward, -length, out z);
            Vector3 x;
            Vector3.Cross(ref upVector, ref z, out x);
            x.Normalize();
            Vector3 y;
            Vector3.Cross(ref z, ref x, out y);

            worldMatrix.M11 = x.X;
            worldMatrix.M12 = x.Y;
            worldMatrix.M13 = x.Z;
            worldMatrix.M14 = fp._0;
            worldMatrix.M21 = y.X;
            worldMatrix.M22 = y.Y;
            worldMatrix.M23 = y.Z;
            worldMatrix.M24 = fp._0;
            worldMatrix.M31 = z.X;
            worldMatrix.M32 = z.Y;
            worldMatrix.M33 = z.Z;
            worldMatrix.M34 = fp._0;

            worldMatrix.M41 = position.X;
            worldMatrix.M42 = position.Y;
            worldMatrix.M43 = position.Z;
            worldMatrix.M44 = fp._1;

        }


        /// <summary>
        /// Creates a world matrix pointing from a position to a target with the given up vector.
        /// </summary>
        /// <param name="position">Position of the transform.</param>
        /// <param name="forward">Forward direction of the transformation.</param>
        /// <param name="upVector">Up vector which is crossed against the forward vector to compute the transform's basis.</param>
        /// <returns>World matrix.</returns>
        public static Matrix CreateWorldRH(Vector3 position, Vector3 forward, Vector3 upVector)
        {
            Matrix lookat;
            CreateWorldRH(ref position, ref forward, ref upVector, out lookat);
            return lookat;
        }



        /// <summary>
        /// Creates a matrix representing a translation.
        /// </summary>
        /// <param name="translation">Translation to be represented by the matrix.</param>
        /// <param name="translationMatrix">Matrix representing the given translation.</param>
        public static void CreateTranslation(ref Vector3 translation, out Matrix translationMatrix)
        {
            translationMatrix = new Matrix
            {
                M11 = fp._1,
                M22 = fp._1,
                M33 = fp._1,
                M44 = fp._1,
                M41 = translation.X,
                M42 = translation.Y,
                M43 = translation.Z
            };
        }

        /// <summary>
        /// Creates a matrix representing a translation.
        /// </summary>
        /// <param name="translation">Translation to be represented by the matrix.</param>
        /// <returns>Matrix representing the given translation.</returns>
        public static Matrix CreateTranslation(Vector3 translation)
        {
            Matrix translationMatrix;
            CreateTranslation(ref translation, out translationMatrix);
            return translationMatrix;
        }

        /// <summary>
        /// Creates a matrix representing the given axis aligned scale.
        /// </summary>
        /// <param name="scale">Scale to be represented by the matrix.</param>
        /// <param name="scaleMatrix">Matrix representing the given scale.</param>
        public static void CreateScale(ref Vector3 scale, out Matrix scaleMatrix)
        {
            scaleMatrix = new Matrix
                {
                    M11 = scale.X,
                    M22 = scale.Y,
                    M33 = scale.Z,
                    M44 = fp._1
			};
        }

        /// <summary>
        /// Creates a matrix representing the given axis aligned scale.
        /// </summary>
        /// <param name="scale">Scale to be represented by the matrix.</param>
        /// <returns>Matrix representing the given scale.</returns>
        public static Matrix CreateScale(Vector3 scale)
        {
            Matrix scaleMatrix;
            CreateScale(ref scale, out scaleMatrix);
            return scaleMatrix;
        }

        /// <summary>
        /// Creates a matrix representing the given axis aligned scale.
        /// </summary>
        /// <param name="x">Scale along the x axis.</param>
        /// <param name="y">Scale along the y axis.</param>
        /// <param name="z">Scale along the z axis.</param>
        /// <param name="scaleMatrix">Matrix representing the given scale.</param>
        public static void CreateScale(fp x, fp y, fp z, out Matrix scaleMatrix)
        {
            scaleMatrix = new Matrix
            {
                M11 = x,
                M22 = y,
                M33 = z,
                M44 = fp._1
			};
        }

        /// <summary>
        /// Creates a matrix representing the given axis aligned scale.
        /// </summary>
        /// <param name="x">Scale along the x axis.</param>
        /// <param name="y">Scale along the y axis.</param>
        /// <param name="z">Scale along the z axis.</param>
        /// <returns>Matrix representing the given scale.</returns>
        public static Matrix CreateScale(fp x, fp y, fp z)
        {
            Matrix scaleMatrix;
            CreateScale(x, y, z, out scaleMatrix);
            return scaleMatrix;
        }

        /// <summary>
        /// Creates a string representation of the matrix.
        /// </summary>
        /// <returns>A string representation of the matrix.</returns>
        public override string ToString()
        {
            return "{" + M11 + ", " + M12 + ", " + M13 + ", " + M14 + "} " +
                   "{" + M21 + ", " + M22 + ", " + M23 + ", " + M24 + "} " +
                   "{" + M31 + ", " + M32 + ", " + M33 + ", " + M34 + "} " +
                   "{" + M41 + ", " + M42 + ", " + M43 + ", " + M44 + "}";
        }
    }
}
