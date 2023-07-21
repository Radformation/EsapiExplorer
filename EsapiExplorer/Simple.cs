namespace EsapiExplorer
{
    // Represents a non-expandable tree item
    public class Simple : IObject
    {
        private readonly object obj;

        public Simple(object obj, string name)
        {
            this.obj = obj;
            this.Name = name;
        }

        public string Name { get; }

        public string Value
        {
            get
            {
                if (obj == null)
                    return "(null)";

                // Show strings with quotes
                if (obj is string str)
                    return $"\"{str}\"";

                // Convert all other types (which should be primitives) to a string
                return obj.ToString();
            }
        }
    }
}