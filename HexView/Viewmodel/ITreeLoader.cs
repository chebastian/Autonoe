using HexView.Model;
using System.Collections.Generic;
using System;

namespace HexView.Viewmodel
{
    public interface ITreeLoader
    {
        List<ITreeNode> LoadSiblings(ITreeNode parent);
    }

    public class FileTreeLoader : ITreeLoader
    {
        public List<ITreeNode> LoadSiblings(ITreeNode parent)
        {
            var list = new List<ITreeNode>();
            foreach (var view in parent.Children)
                list.Add(new FileTreeNode((view as FileTreeNode).RootName,parent));

            return list;

        }
    }
}