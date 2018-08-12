using System;
using GameArchitect.Design;
using GameArchitect.Design.Metadata;
using GameArchitect.Design.Primitives;
using GameArchitect.Tasks.CodeGeneration.CXX;

namespace GameArchitect.Tasks.CodeGeneration.Unreal
{
    public class UnrealTypeTransformer : CXXTypeTransformer
    {
        public UnrealTypeTransformer(UnrealNameTransformer nameTransformer) 
            : base(nameTransformer) { }

        public override string TransformType(IMemberInfo member)
        {
            var result = TransformType(member.Type);
            if (member.IsOptional)
                result = $"TOptional<{result}>";

            if (member.CollectionType != CollectionType.None)
            {
                switch (member.CollectionType)
                {
                    case CollectionType.List:
                        result = $"TArray<{result}>";
                        break;

                    case CollectionType.Dictionary:
                        throw new NotImplementedException();
                        break;

                    case CollectionType.Queue:
                        result = $"TQueue<{result}>";
                        break;

                    case CollectionType.Stack:
                        result = $"TStack<{result}>";
                        break;
                }
            }

            return result;
        }

        public override string TransformType(ITypeInfo type)
        {
            var result = base.TransformType(type);

            var nativeType = type.Native;
            if (nativeType == typeof(string))
                result = "FString";
            else if (nativeType == typeof(Vector3<float>) || nativeType == typeof(Vector3<double>))
                result = "FVector";
            else if (nativeType == typeof(Vector2<float>) || nativeType == typeof(Vector2<double>))
                result = "FVector2D";
            else if (nativeType == typeof(Vector4<float>) || nativeType == typeof(Vector4<double>))
                result = "FVector4";
            else if (nativeType == typeof(Quaternion<float>) || nativeType == typeof(Quaternion<double>))
                result = "FQuat";
            else if (nativeType == typeof(Rotation<float>) || nativeType == typeof(Rotation<double>))
                result = "FRotator";
            else if (nativeType == typeof(Box<float>) || nativeType == typeof(Box<double>))
                result = "FBox";
            else if (nativeType == typeof(Rect<float>) || nativeType == typeof(Rect<double>))
                result = "FBox2D";
            else
                result = NameTransformer.TransformName(type, type.Name, NameContext.Type);
            result = result.Trim();

            if(string.IsNullOrEmpty(result))
                throw new NotSupportedException($"Type {type.GetPath()} not resolved in {nameof(UnrealTypeTransformer)}.");

            return result;
        }
    }
}