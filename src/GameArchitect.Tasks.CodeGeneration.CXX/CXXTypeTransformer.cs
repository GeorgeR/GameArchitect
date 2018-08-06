using System;
using GameArchitect.Design;
using GameArchitect.Design.Metadata;

namespace GameArchitect.Tasks.CodeGeneration.CXX
{
    public class CXXTypeTransformer : ITypeTransformer
    {
        protected INameTransformer NameTransformer { get; }

        public CXXTypeTransformer(INameTransformer nameTransformer)
        {
            NameTransformer = nameTransformer;
        }

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

        public virtual string TransformType(ITypeInfo type)
        {
            var result = string.Empty;

            var nativeType = type.Native;
            if (nativeType == typeof(string))
                result = "std::string";
            else if (nativeType == typeof(sbyte))
                result = "int8";
            else if (nativeType == typeof(byte))
                result = "uint8";
            else if (nativeType == typeof(short))
                result = "int16";
            else if (nativeType == typeof(ushort))
                result = "uint16";
            else if (nativeType == typeof(int))
                result = "int32";
            else if (nativeType == typeof(uint))
                result = "uint32";
            else if (nativeType == typeof(long))
                result = "int64";
            else if (nativeType == typeof(ulong))
                result = "uint64";
            else if (nativeType == typeof(bool))
                result = "bool";
            else if (nativeType == typeof(void))
                result = "void";
            else if (nativeType == typeof(float))
                result = "float";
            else if (nativeType == typeof(double))
                result = "double";
            else
                result = NameTransformer.TransformName(type, type.Name, NameContext.Type);

            return result;
        }
    }
}