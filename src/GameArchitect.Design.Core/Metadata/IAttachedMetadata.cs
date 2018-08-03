using System;

namespace GameArchitect.Design.Metadata
{
    //public interface IAttachedMetadata
    //{
    //    IMetaInfo Info { get; }

    //    string TransformName();
    //    string TransformType();
    //}

    //public abstract class AttachedMetadataBase : IAttachedMetadata
    //{
    //    public IMetaInfo Info { get; }

    //    protected INameTransformer NameTransformer { get; }
    //    protected ITypeTransformer TypeTransformer { get; }

    //    protected abstract NameContext NameContext { get; set; }

    //    protected AttachedMetadataBase(INameTransformer nameTransformer, ITypeTransformer typeTransformer, IMetaInfo info)
    //    {
    //        NameTransformer = nameTransformer;
    //        TypeTransformer = typeTransformer;

    //        Info = info;
    //    }

    //    public virtual string TransformName()
    //    {
    //        return NameTransformer.TransformName(Info, Info.Name, NameContext);
    //    }

    //    public virtual string TransformType()
    //    {
    //        if(Info is TypeInfo info)
    //            return TypeTransformer.TransformType(info);

    //        if (Info is IMemberInfo memberInfo)
    //            return TypeTransformer.TransformType(memberInfo);

    //        throw new Exception($"Info {Info.GetPath()} not supported by TypeTransformer.");
    //    }
    //}
}