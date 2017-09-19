using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor.IMGUI.Controls;

namespace Assets.Weapons
{
    public class MovesetTreeview : TreeView
    {
        private ArrayThatWorksForActions _moveset;
        private string _nameOfYaMoves;
        public MovesetTreeview(TreeViewState state, ArrayThatWorksForActions moveset, string nameOfYaMoves) : base(state) 
        {
            Reload();
            _moveset = moveset;
            _nameOfYaMoves = nameOfYaMoves;
        }
        protected override TreeViewItem BuildRoot()
        {
            var root = new TreeViewItem(0,-1, _nameOfYaMoves);

            return root;
        }
    }
}
