// The origin of this class is the Matrixs4x4.cs file that Joa Ebert shared on his blog:
// http://blog.joa-ebert.com/2009/08/10/flirting-with-silverlight/
using System;

namespace Robot.Kinematics
{
    public sealed class Matrix4x4
    {
        public double I00;
        public double I01;
        public double I02;
        public double I03;
        public double I10;
        public double I11;
        public double I12;
        public double I13;
        public double I20;
        public double I21;
        public double I22;
        public double I23;
        public double I30;
        public double I31;
        public double I32;
        public double I33;

		public static readonly Matrix4x4 Identity = new Matrix4x4(1);

        public static Matrix4x4 Translation(double x, double y, double z)
        {
            var result = new Matrix4x4();

            result.I03 = x;
            result.I13 = y;
            result.I23 = z;

            return result;
        }

        public static Matrix4x4 Scale(double x, double y, double z)
        {
            var result = new Matrix4x4();

            result.I00 = x;
            result.I11 = y;
            result.I22 = z;

            return result;
        }

        public static Matrix4x4 RotationX(double radiants)
        {
            var result = new Matrix4x4();

            var cos = Math.Cos(radiants);
            var sin = Math.Sin(radiants);

            result.I11 = cos;
            result.I12 = -sin;
            result.I21 = sin;
            result.I22 = cos;

            return result;
        }

        public static Matrix4x4 RotationY(double radiants)
        {
            var result = new Matrix4x4();

            var cos = Math.Cos(radiants);
            var sin = Math.Sin(radiants);

            result.I00 = cos;
            result.I02 = -sin;
            result.I20 = sin;
            result.I22 = cos;

            return result;
        }

        public static Matrix4x4 operator *(Matrix4x4 lhs, Matrix4x4 rhs)
        {
            var result = new Matrix4x4();

            result.I00 = lhs.I00 * rhs.I00 + lhs.I01 * rhs.I10 + lhs.I02 * rhs.I20;
            result.I01 = lhs.I00 * rhs.I01 + lhs.I01 * rhs.I11 + lhs.I02 * rhs.I21;
            result.I02 = lhs.I00 * rhs.I02 + lhs.I01 * rhs.I12 + lhs.I02 * rhs.I22;
            result.I03 = lhs.I00 * rhs.I03 + lhs.I01 * rhs.I13 + lhs.I02 * rhs.I23 + lhs.I03;

            result.I10 = lhs.I10 * rhs.I00 + lhs.I11 * rhs.I10 + lhs.I12 * rhs.I20;
            result.I11 = lhs.I10 * rhs.I01 + lhs.I11 * rhs.I11 + lhs.I12 * rhs.I21;
            result.I12 = lhs.I10 * rhs.I02 + lhs.I11 * rhs.I12 + lhs.I12 * rhs.I22;
            result.I13 = lhs.I10 * rhs.I03 + lhs.I11 * rhs.I13 + lhs.I12 * rhs.I23 + lhs.I13;

            result.I20 = lhs.I20 * rhs.I00 + lhs.I21 * rhs.I10 + lhs.I22 * rhs.I20;
            result.I21 = lhs.I20 * rhs.I01 + lhs.I21 * rhs.I11 + lhs.I22 * rhs.I21;
            result.I22 = lhs.I20 * rhs.I02 + lhs.I21 * rhs.I12 + lhs.I22 * rhs.I22;
            result.I23 = lhs.I20 * rhs.I03 + lhs.I21 * rhs.I13 + lhs.I22 * rhs.I23 + lhs.I23;

            result.I30 = lhs.I30 * rhs.I00 + lhs.I31 * rhs.I10 + lhs.I32 * rhs.I20;
            result.I31 = lhs.I30 * rhs.I01 + lhs.I31 * rhs.I11 + lhs.I32 * rhs.I21;
            result.I32 = lhs.I30 * rhs.I02 + lhs.I31 * rhs.I12 + lhs.I32 * rhs.I22;
            result.I33 = lhs.I30 * rhs.I03 + lhs.I31 * rhs.I13 + lhs.I32 * rhs.I23 + lhs.I33;

            return result;
        }

		public static Vector4 operator *(Matrix4x4 matrix, Vector4 vector)
		{
			return new Vector4(
				matrix.I00 * vector.V0 + matrix.I01 * vector.V1 + matrix.I02 * vector.V2 + matrix.I03 * vector.V3,
				matrix.I10 * vector.V0 + matrix.I11 * vector.V1 + matrix.I12 * vector.V2 + matrix.I13 * vector.V3,
				matrix.I20 * vector.V0 + matrix.I21 * vector.V1 + matrix.I22 * vector.V2 + matrix.I23 * vector.V3,
				matrix.I30 * vector.V0 + matrix.I31 * vector.V1 + matrix.I32 * vector.V2 + matrix.I33 * vector.V3
				);
		}

        public static Matrix4x4 operator +(Matrix4x4 lhs, Matrix4x4 rhs)
        {
            var result = new Matrix4x4();

            for (int i = 0; i < 15; ++i)
                result[i] = lhs[i] + rhs[i];

            return result;
        }

        public Matrix4x4() : this(1.0) { }

        public Matrix4x4(double diagonal)
            : this(
                diagonal, 0.0, 0.0, 0.0,
                0.0, diagonal, 0.0, 0.0,
                0.0, 0.0, diagonal, 0.0,
                0.0, 0.0, 0.0, diagonal
            ) { }

        public Matrix4x4(double[] values)
            : this(
                values[0], values[1], values[2], values[3],
                values[4], values[5], values[6], values[7],
                values[8], values[9], values[10], values[11],
                values[12], values[13], values[14], values[15]
            ) { }

        public Matrix4x4(Matrix4x4 matrix4x4)
            : this(
                matrix4x4.I00, matrix4x4.I01, matrix4x4.I02, matrix4x4.I03,
                matrix4x4.I10, matrix4x4.I11, matrix4x4.I12, matrix4x4.I13,
                matrix4x4.I20, matrix4x4.I21, matrix4x4.I22, matrix4x4.I23,
                matrix4x4.I30, matrix4x4.I31, matrix4x4.I32, matrix4x4.I33
            ) { }

        public Matrix4x4(
            double i00, double i01, double i02, double i03,
            double i10, double i11, double i12, double i13,
            double i20, double i21, double i22, double i23,
            double i30, double i31, double i32, double i33
            )
        {
            I00 = i00;
            I01 = i01;
            I02 = i02;
            I03 = i03;

            I10 = i10;
            I11 = i11;
            I12 = i12;
            I13 = i13;

            I20 = i20;
            I21 = i21;
            I22 = i22;
            I23 = i23;

            I30 = i30;
            I31 = i31;
            I32 = i32;
            I33 = i33;
        }

        public void SetIdentity()
        {
            I00 = I11 = I22 = I33 = 1.0;
            I01 = I02 = I03 = I10 = I12 = I13 = I20 = I21 = I23 = I30 = I31 = I32 = 0.0;
        }

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return I00;
                    case 1: return I01;
                    case 2: return I02;
                    case 3: return I03;
                    case 4: return I10;
                    case 5: return I11;
                    case 6: return I12;
                    case 7: return I13;
                    case 8: return I20;
                    case 9: return I21;
                    case 10: return I22;
                    case 11: return I23;
                    case 12: return I30;
                    case 13: return I31;
                    case 14: return I32;
                    case 15: return I33;
                    default: return 0.0;
                }
            }
            set
            {
                switch (index)
                {
                    case 0: I00 = value; break;
                    case 1: I01 = value; break;
                    case 2: I02 = value; break;
                    case 3: I03 = value; break;
                    case 4: I10 = value; break;
                    case 5: I11 = value; break;
                    case 6: I12 = value; break;
                    case 7: I13 = value; break;
                    case 8: I20 = value; break;
                    case 9: I21 = value; break;
                    case 10: I22 = value; break;
                    case 11: I23 = value; break;
                    case 12: I30 = value; break;
                    case 13: I31 = value; break;
                    case 14: I32 = value; break;
                    case 15: I33 = value; break;
                }
            }
        }

        public Matrix4x4 Transpose()
        {
            return new Matrix4x4(
                I00, I10, I20, I30,
                I01, I11, I21, I31,
                I02, I12, I22, I32,
                I03, I13, I23, I33);
        }
    }
}
