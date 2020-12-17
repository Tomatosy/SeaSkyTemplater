using System;
using System.Data;
using System.Text;

namespace Tomato.StandardLib.DAL.Base
{
    /// <summary>
    /// 列信息
    /// </summary>
    public class ColumnInfo
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="typeName"></param>
        /// <param name="width"></param>
        /// <param name="scale"></param>
        /// <param name="nullable"></param>
        public ColumnInfo(string columnName, string typeName, int width, int scale, int nullable)
        {
            this.ColumnName = columnName;
            this.TypeName = typeName;
            this.IsNullable = (nullable == 1);
            this.Translate(typeName, width, scale);
        }

        internal string ColumnName { get; set; }
        internal string TypeName { get; set; }
        internal int Scale { get; private set; }
        internal int Size { get; private set; }
        internal SqlDbType DbType { get; private set; }
        internal int Precision { get; private set; }
        internal bool IsNullable { get; private set; }
        internal bool UseSize { get; private set; }
        internal bool UseScale { get; private set; }
        internal bool UsePrecision { get; private set; }

        private void Translate(string typeName, int width, int scale)
        {
            switch (typeName)
            {
                case "tinyint":
                    this.DbType = SqlDbType.TinyInt;
                    break;
                case "smallint":
                    this.DbType = SqlDbType.SmallInt;
                    break;
                case "integer":
                    this.DbType = SqlDbType.Int;
                    break;
                case "int":
                    this.DbType = SqlDbType.Int;
                    break;
                case "bigint":
                    this.DbType = SqlDbType.BigInt;
                    break;
                case "numeric":
                    this.DbType = SqlDbType.Decimal;
                    this.Precision = width;
                    this.Scale = scale;
                    this.UsePrecision = true;
                    this.UseScale = true;
                    break;
                case "decimal":
                    this.DbType = SqlDbType.Decimal;
                    this.Precision = width;
                    this.Scale = scale;
                    this.UsePrecision = true;
                    this.UseScale = true;
                    break;
                case "unsigned smallint":
                    // overflow
                    this.DbType = SqlDbType.SmallInt;
                    break;
                case "unsigned int":
                    // overflow
                    this.DbType = SqlDbType.Int;
                    break;
                case "unsigned bigint":
                    // overflow
                    this.DbType = SqlDbType.BigInt;
                    break;
                case "float":
                    this.DbType = SqlDbType.Real;
                    break;
                case "double":
                    this.DbType = SqlDbType.Float;
                    break;
                case "date":
                    this.DbType = SqlDbType.Date;
                    break;
                case "time":
                    this.DbType = SqlDbType.Time;
                    break;
                case "timestamp":
                    this.DbType = SqlDbType.DateTime2;
                    break;
                case "datetime":
                case "datetime2":
                    this.DbType = SqlDbType.DateTime2;
                    break;
                case "uniqueidentifier":
                    this.DbType = SqlDbType.UniqueIdentifier;
                    break;
                case "bit":
                    this.DbType = SqlDbType.Bit;
                    break;
                case "binary":
                case "varbinary":
                case "long binary":
                case "varbit":
                    this.Size = (width > 8000) ? -1 : width;
                    this.DbType = SqlDbType.Binary;
                    this.UseSize = true;
                    break;
                case "long varbit":
                    this.Size = -1;
                    this.DbType = SqlDbType.Binary;
                    this.UseSize = true;
                    break;
                case "xml":
                    this.DbType = SqlDbType.Xml;
                    break;
                case "char":
                    this.Size = width;
                    this.DbType = SqlDbType.Char;
                    this.UseSize = true;

                    if (width > 4000)
                    {
                        this.TypeName = "varchar";
                        this.DbType = SqlDbType.NVarChar;
                        this.Size = -1;
                        this.UseSize = true;
                    }

                    break;
                case "varchar":
                    this.Size = (width > 4000) ? -1 : width;
                    this.DbType = SqlDbType.VarChar;
                    this.UseSize = true;
                    break;
                case "nchar":
                    this.Size = width;
                    this.DbType = SqlDbType.NChar;
                    this.UseSize = true;
                    break;
                case "nvarchar":
                    this.Size = (width > 4000) ? -1 : width;
                    this.DbType = SqlDbType.NVarChar;
                    this.UseSize = true;
                    break;
                case "long varchar":
                    this.Size = -1;
                    this.DbType = SqlDbType.VarChar;
                    this.UseSize = true;
                    break;
                case "long nvarchar":
                    this.Size = -1;
                    this.DbType = SqlDbType.NVarChar;
                    this.UseSize = true;
                    break;
                case "ntext":
                    this.DbType = SqlDbType.NText;
                    break;
                default:
                    Console.WriteLine(typeName);
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// 转成string字符串
        /// </summary>
        /// <returns>ColumnName TypeName DbType Size Precision Scale IsNullable UseScale UseSize</returns>
        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}", this.ColumnName, this.TypeName, this.DbType, this.Size, this.Precision, this.Scale, this.IsNullable, this.UseScale, this.UseSize);
        }

        internal string GetSqlColumnDefinition()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            sb.Append(this.ColumnName);
            sb.Append("] ");

            string newType;
            string newLength = "";
            string newScale = "";

            switch (this.TypeName.ToLower())
            {
                case "char":
                case "nchar":
                    newType = "nchar";
                    newLength = this.Size.ToString();

                    if (this.Size > 4000)
                    {
                        newType = "nvarchar";
                        newLength = (this.Size == -1 || this.Size > 4000) ? "max" : this.Size.ToString();
                    }
                    break;
                case "date":
                    newType = this.TypeName;
                    break;
                case "integer":
                    newType = this.TypeName;
                    break;
                case "long varchar":
                    newType = "nvarchar";
                    newLength = "max";
                    break;
                case "numeric":
                    newType = this.TypeName;
                    newLength = this.Precision.ToString();
                    newScale = this.Scale.ToString();
                    break;
                case "smallint":
                    newType = this.TypeName;
                    break;
                case "time":
                    newType = this.TypeName;
                    break;
                case "timestamp":
                    newType = "datetime2";
                    break;
                case "tinyint":
                    newType = this.TypeName;
                    break;
                case "varchar":
                case "nvarchar":
                    newType = "nvarchar";
                    newLength = (this.Size == -1 || this.Size > 4000) ? "max" : this.Size.ToString();
                    break;
                default:
                    newType = this.TypeName;
                    break;
            }

            sb.Append(newType);


            if (this.UseSize || this.UsePrecision)
            {
                sb.Append("(");

                sb.Append(newLength);

                if (this.UseScale)
                {
                    sb.Append(",");
                    sb.Append(newScale);
                }

                sb.Append(")");
            }

            if (!this.IsNullable)
            {
                sb.Append(" NOT NULL");
            }

            return sb.ToString();
        }
    }
}
