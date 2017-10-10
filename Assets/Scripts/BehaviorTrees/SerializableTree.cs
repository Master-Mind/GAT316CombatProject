using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FullSerializer;
using UnityEngine;

namespace Assets.Scripts.BehaviorTrees
{
    //This is a standin for when the behavior tree is getting edited/loaded
    public class SerializableTree : ScriptableObject
    {
        //the children of this node. Order of the children matters
        public List<SerializableTree> Children;
        //The type of behavior tree node that this "tree" node represents
        public Type MyNodeType;
        //The per-node data for this node. Can be null if the node type doesn't need such data
        public BTNodeData MyData;
        //The type of the data
        public Type MyDataType;

        //Handles the creation of the node. Shouldn't be called if this tree was made from GetTreeData
        public void Initialize(Type nodeType)
        {
            Children = new List<SerializableTree>();
            MyNodeType = nodeType;
            var dataField = MyNodeType.GetField("DataType");
            if (dataField != null)
            {
                MyDataType = (Type)dataField.GetValue(null);
            }
            
        }

        //Convert dis boi into json
        public string SerializeTreeData(bool compressJson)
        {
            fsData data;
            fsSerializer serializer = new fsSerializer();

            serializer.TrySerialize(this, out data);

            if (compressJson)
            {
                return fsJsonPrinter.CompressedJson(data);
            }
            else
            {
                return fsJsonPrinter.CompressedJson(data);
            }
        }

        //Make a new boi from json, you do not need to call initialize on each node
        public static SerializableTree GetTreeDataForEditor(string jsonData)
        {
            SerializableTree ret = new SerializableTree();
            fsData data = fsJsonParser.Parse(jsonData);
            fsSerializer serializer = new fsSerializer();

            serializer.TryDeserialize(data, ref ret);

            return ret;
        }
    }
}
