using System;
using GameArchitect.Design;
using GameArchitect.Design.Metadata;

namespace GameArchitect.Tasks.CodeGeneration.CXX
{
    public class CXXTypeTransformer : ITypeTransformer
    {
        public virtual string TransformType(IMemberInfo member)
        {
            var result = TransformType(member.Type);
            if (member.IsOptional)
                result = $"std::optional<{result}>";

            if (member.CollectionType != CollectionType.None)
            {
                switch (member.CollectionType)
                {
                    case CollectionType.List:
                        result = $"std::vector<{result}>";
                        break;

                    case CollectionType.Dictionary:
                        throw new NotImplementedException();
                        break;

                    case CollectionType.Queue:
                        result = $"std::queue<{result}>";
                        break;

                    case CollectionType.Stack:
                        result = $"std::stack<{result}>";
                        break;
                }
            }
            
            return result;
        }

        public virtual string TransformType(TypeInfo type)
        {
            var result = string.Empty;
            if (type == typeof(string))
                result = "std::string";
            else if (type == typeof(sbyte))
                result = "int8";
            else if (type == typeof(byte))
                result = "uint8";
            else if (type == typeof(short))
                result = "int16";
            else if (type == typeof(ushort))
                result = "uint16";
            else if (type == typeof(int))
                result = "int32";
            else if (type == typeof(uint))
                result = "uint32";
            else if (type == typeof(long))
                result = "int64";
            else if (type == typeof(ulong))
                result = "uint64";
            else if (type == typeof(bool))
                result = "bool";
            else if (type == typeof(void))
                result = "void";
            else if (type == typeof(float))
                result = "float";
            else if (type == typeof(double))
                result = "double";

            return result;
        }
    }
}