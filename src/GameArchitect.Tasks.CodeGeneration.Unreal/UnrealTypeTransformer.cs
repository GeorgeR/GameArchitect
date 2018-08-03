using System;
using GameArchitect.Design;
using GameArchitect.Design.Metadata;
using GameArchitect.Design.Primitives;
using GameArchitect.Tasks.CodeGeneration.CXX;

namespace GameArchitect.Tasks.CodeGeneration.Unreal
{
    public class UnrealTypeTransformer : CXXTypeTransformer
    {
        public UnrealTypeTransformer(INameTransformer nameTransformer) 
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

        public override string TransformType(TypeInfo type)
        {
            var result = base.TransformType(type);

            if (type == typeof(string))
                result = "FString";
            else if (type == typeof(Vector3<float>) || type == typeof(Vector3<double>))
                result = "FVector";
            else if (type == typeof(Vector2<float>) || type == typeof(Vector2<double>))
                result = "FVector2D";
            else if (type == typeof(Vector4<float>) || type == typeof(Vector4<double>))
                result = "FVector4";
            else if (type == typeof(Quaternion<float>) || type == typeof(Quaternion<double>))
                result = "FQuat";
            else if (type == typeof(Rotation<float>) || type == typeof(Rotation<double>))
                result = "FRotator";
            else if (type == typeof(Box<float>) || type == typeof(Box<double>))
                result = "FBox";
            else if (type == typeof(Rect<float>) || type == typeof(Rect<double>))
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