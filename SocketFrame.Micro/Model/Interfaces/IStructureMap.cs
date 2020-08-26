using System;

namespace Kana.Ikimi.SocketFrame.Micro.Model.Interfaces {

    public interface IStructureMap {

        Type For { get; }
        Type[] Injecting { get; }
        Type Using { get; }
        StructureMap Use(Type mapping);
        StructureMap Inject(params Type[] typeList);
        void UseDefault();

    }

}