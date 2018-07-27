using System;
using GameArchitect.Design;
using GameArchitect.Design.Metadata;
using GameArchitect.Design.Primitives;
using GameArchitect.Tasks.CodeGeneration.CXX;

namespace GameArchitect.Tasks.CodeGeneration.Unreal
{
    public class UnrealTypeTransformer : CXXTypeTransformer
    {
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
            else if (type == typeof(Vector3<float>))
                result = "FVector";
            else if (type == typeof(Vector2<float>))
                result = "FVector2D";
            else if (type == typeof(Vector3<float>))
                result = "FVector4";
            else if (type == typeof(Quaternion<float>))
                result = "FQuat";
            else if (type == typeof(Rotation<float>))
                result = "FRotator";
            else if (type == typeof(Box<float>))
                result = "FBox";
            else if (type == typeof(Rect<float>))
                result = "FBox2D";

            return result;
        }
    }
}