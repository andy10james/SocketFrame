using System;
using Kana.Ikimi.SocketFrame.Micro.Model.Interfaces;

namespace Kana.Ikimi.SocketFrame.Micro.Scaffolding.Interfaces {

    public interface IStructureBroker {

        IStructureMap For(Type type);
        IStructureMap Find(Type type);
        void CheckForUninstanciables();
        void CheckForCircularDependency();
        Object InstanciateFor(Type type);
        Object[] InstanciateFor(Type[] types);

    }

}