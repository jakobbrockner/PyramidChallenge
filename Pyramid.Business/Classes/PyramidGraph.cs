using System;
using System.Collections.Generic;
using System.Linq;
using Pyramid.Core.Extensions;
using Pyramid.Core.Interfaces;

namespace Pyramid.Business.Classes
{
    public class PyramidGraph
    {
        private List<int[]> pyramidValueRaw = new List<int[]>();
        private readonly IValuesReader PyramidValuesReader;
        private readonly int numberOfChildNodes;
        private readonly bool forceOddEvenRule;
        private readonly  List<int> pyramidPathItems = new List<int>();
        private int max = 0;
        private PyramidItem pyramidRootItem;

        public int Max
        {
            get
            {
                return this.max;
            }
        }

        public List<int> PyramidPathItems
        {
            get
            {
                return pyramidPathItems;
            }

        }

        public PyramidGraph(IValuesReader valueReader, int numberOfChildNodes, bool forceOddEvenRule)
        {
            this.PyramidValuesReader = valueReader;
            this.numberOfChildNodes = numberOfChildNodes;
            this.forceOddEvenRule = forceOddEvenRule;
        }

        private void GetValues()
        {
            //
            //Only get values if we dont have any
            //
            if (this.pyramidValueRaw.Count == 0)
            {
                this.pyramidValueRaw = PyramidValuesReader.ReturnValues();
            }

        }

        public void CreatePath()
        {
            if (this.pyramidRootItem == null)
            {
                throw new Exception("Please call Build() before CreatePath()");
            }
            pyramidPathItems.Add(this.pyramidRootItem.Value);
            FindNextNodes(this.pyramidRootItem);
            this.max = this.pyramidPathItems.Sum();
        }


        private void FindNextNodes(PyramidItem parentPyramidItem)
        {
            //
            //from childnodes retrieve node with highest value and that has opposite even-odd than parent value
            //
            PyramidItem node = null;
            if (this.forceOddEvenRule)
            {
                node = parentPyramidItem.Nodes.FindAll(x => x.Value.IsEvenNumber() ^ parentPyramidItem.Value.IsEvenNumber())
                   .OrderByDescending(x => x.Value).FirstOrDefault();
            }
            else
            {
                node = parentPyramidItem.Nodes.OrderByDescending(x => x.Value).FirstOrDefault();
            }
            if (node == null)
            {
                return;
            }
            pyramidPathItems.Add(node.Value);
            FindNextNodes(node);
        }

        public void Build()
        {
            GetValues();
            //
            //Add first item from raw values.
            //
            this.pyramidRootItem = new PyramidItem()
            {
                x = 0,
                y = 0,
                Value = pyramidValueRaw[0][0],
                Nodes = new List<PyramidItem>()
            };

            //
            //Add childNodes to current Node recursive
            //
            AddChildNodes(this.pyramidRootItem, this.pyramidValueRaw);

            return;
        }

        private void AddChildNodes(PyramidItem pyramidItem, List<int[]> values)
        {
            var xPos = pyramidItem.x;
            var yPos = pyramidItem.y + 1; //<- Goto Next line of values
            if (yPos >= values.Count)
            {
                return;
            }

            //
            // Get child nodes. Continue until number of child nodes has been retrieved
            //
            for (int i = 0; i < this.numberOfChildNodes; i++)
            {
                pyramidItem.Nodes.Add(new PyramidItem()
                {
                    Nodes = new List<PyramidItem>(),
                    x = xPos,
                    y = yPos,
                    Value = pyramidValueRaw[yPos][xPos],
                });
                xPos++;
            }
            //
            // Add Child Nodes recursive
            //
            foreach (var pyramidItemNode in pyramidItem.Nodes)
            {
                AddChildNodes(pyramidItemNode, values);
            }

        }
    }
}