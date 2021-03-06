﻿using DataMapper.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace DataMapper
{
    public static class Helpers
    {
        public static IEnumerable<PropertyInfo> GetProperties<TAttribute>(Type type)
        {
            return type.GetProperties()
                .Where(p => p.IsDefined(typeof(TAttribute), false));
        }

        public static void SetValue(object entity, PropertyInfo property, object value)
        {
            if (value == DBNull.Value)
            {
                value = null;
            }
            property.SetValue(entity, value);
        }

        public static bool IsNullable(Type type)
        {
            if (!type.IsValueType)
            {
                return true;
            }

            if (Nullable.GetUnderlyingType(type) != null)
            {
                return true;
            }

            return false;
        }

        public static Type ExtractTypeFromNullable(Type type)
        {
            if (!type.IsValueType)
            {
                return type;
            }

            var underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
            {
                return underlyingType;
            }

            return type;
        }

        public static List<string> GetColumnNames(DataTable dataTable)
        {
            var columns = new List<string>();
            foreach (DataColumn column in dataTable.Columns)
            {
                columns.Add(column.ColumnName);
            }
            return columns;
        }

        public static List<string> GetColumnNames(IDataReader dataReader)
        {
            var columns = new List<string>();
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                columns.Add(dataReader.GetName(i));
            }
            return columns;
        }
    }
}