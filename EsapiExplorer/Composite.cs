using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiExplorer
{
    // Represents an expandable tree item
    public class Composite : IObject
    {
        private readonly object obj;

        public Composite(object obj, string name)
        {
            this.obj = obj;
            this.Name = name;
        }

        public string Name { get; }

        // Summary value to show next to the name
        public string Preview
        {
            get
            {
                // If it's a list, show a count
                if (obj is IEnumerable enumerable)
                    return $"(count: {enumerable.Cast<object>().Count()})";

                // If it's a date/time, show its string representation
                if (obj is DateTime dateTime)
                    return $"{dateTime:G}";

                // If it's a Dose, show its string representation
                if (obj is DoseValue dose)
                    return dose.ToString();

                // If it's any other ESAPI object, show its Id
                if (obj is ApiDataObject api)
                    return api.Id;

                // If it's an exception, show its message
                if (obj is Exception ex)
                    return $"(exception: {ex.Message})";

                // For all other types, don't show anything
                return null;
            }
        }

        public IEnumerable<IObject> Items
        {
            get
            {
                // Protect against null, just in case
                if (obj == null)
                    return Enumerable.Empty<IObject>();

                // If this object is a list, convert them to IObjects
                if (obj is IEnumerable enumerable)
                    return enumerable.Cast<object>().Select<object, IObject>((o, i) =>
                    {
                        // Use Simple for enums, primitives, and strings
                        if (o.GetType().IsEnum || o.GetType().IsPrimitive || o is string)
                            return new Simple(o, $"[{i}]");

                        // Use Composite for any other types
                        return new Composite(o, $"[{i}]");
                    });

                // Get all public fields and properties to return as Items
                var fields = obj.GetType().GetFields();
                var properties = obj.GetType().GetProperties();
                var members = fields.Concat<MemberInfo>(properties);

                // Sort the fields/properties first
                return members.OrderBy(m => m.Name).Select<MemberInfo, IObject>(m =>
                {
                    try
                    {
                        // Get field or property value
                        var value = GetValue(obj, m);

                        // Convert to Simple if the value is null, enum, primitive, or a string
                        if (value == null || value.GetType().IsEnum || value.GetType().IsPrimitive || value is string)
                            return new Simple(value, m.Name);

                        // For all other types, convert to Composite
                        return new Composite(value, m.Name);
                    }
                    catch (Exception e)
                    {
                        // Let user examine the exception
                        return new Composite(e, m.Name);
                    }
                });
            }
        }

        private object GetValue(object o, MemberInfo memberInfo)
        {
            if (memberInfo is PropertyInfo p)
                return p.GetValue(o);
            if (memberInfo is FieldInfo f)
                return f.GetValue(o);
            return null;
        }
    }
}