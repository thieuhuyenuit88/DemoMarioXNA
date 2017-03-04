using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FrameWork.FrameWork.Object;
using FrameWork.FrameWork.Graphics;
namespace FrameWork.FrameWork.QuadTree
{
    class QuadTreeNode
    {
        List<MyObject> m_contents = new List<MyObject>();
        Rectangle m_bounds;
        List<QuadTreeNode> m_nodes = new List<QuadTreeNode>(4);

        public List<MyObject> Contents
        {
            get
            {
                List<MyObject> Result = new List<MyObject>();
                Result.AddRange(m_contents);
                m_contents.Clear();
                return Result;
            }
        }

        public QuadTreeNode(Rectangle bounds)
        {
            m_bounds = bounds;
        }

        public Rectangle Bounds
        {
            get { return m_bounds; }
        }

        public int Count
        {
            get
            {
                int count = 0;

                foreach (QuadTreeNode node in m_nodes)
                    count += node.Count;  //đệ quy

                count += this.Contents.Count;

                return count;
            }
        }

        public bool IsEmpty
        {
            get { return m_bounds.IsEmpty || m_nodes.Count == 0; }
        }

        public List<MyObject> SubTreeContents
        {
            get
            {
                List<MyObject> results = new List<MyObject>();

                foreach (QuadTreeNode node in m_nodes)
                    results.AddRange(node.SubTreeContents);

                results.AddRange(this.Contents);
                return results;
            }
        }

        private void CreateSubNodes()
        {
            // the smallest subnode has an area 
            if ((m_bounds.Height * m_bounds.Width) <= 10)
                return;

            int halfWidth = (m_bounds.Width / 2);
            int halfHeight = (m_bounds.Height / 2);

            m_nodes.Add(new QuadTreeNode(new Rectangle(m_bounds.Left, m_bounds.Top, halfWidth, halfHeight)));
            m_nodes.Add(new QuadTreeNode(new Rectangle(m_bounds.Left, m_bounds.Top + halfHeight, halfWidth, halfHeight)));
            m_nodes.Add(new QuadTreeNode(new Rectangle(m_bounds.Left + halfWidth, m_bounds.Top, halfWidth, halfHeight)));
            m_nodes.Add(new QuadTreeNode(new Rectangle(m_bounds.Left + halfWidth, m_bounds.Top + halfHeight, halfWidth, halfHeight)));
        }

        public void Insert(MyObject item)
        {
            if (!m_bounds.Contains(item.RECT))
            {
                return;
            }

            if (m_nodes.Count == 0)
                CreateSubNodes();

            foreach (QuadTreeNode node in m_nodes)
            {
                if (node.Bounds.Contains(item.RECT))
                {
                    node.Insert(item);
                    return;
                }
            }
            this.m_contents.Add(item);
        }

        public List<MyObject> Query(Rectangle queryArea)
        {
            // create a list of the items that are found
            List<MyObject> results = new List<MyObject>();

            // this quad contains items that are not entirely contained by
            // it's four sub-quads. Iterate through the items in this quad 
            // to see if they intersect.
            /*foreach (MyObject item in this.Contents)
            {
                if (queryArea.IntersectsWith(item.Rectangle))
                    results.Add(item);
            }*/
            for (int i = 0; i < m_contents.Count; )
            {
                if (queryArea.Intersects(m_contents[i].RECT))
                {
                    results.Add(m_contents[i]);
                    m_contents.Remove(m_contents[i]);
                }
                else i++;
            }

            foreach (QuadTreeNode node in m_nodes)
            {
                if (node.IsEmpty)
                    continue;

                if (node.Bounds.Contains(queryArea))
                {
                    results.AddRange(node.Query(queryArea));
                    break;
                }

                if (queryArea.Contains(node.Bounds))
                {
                    results.AddRange(node.SubTreeContents);
                    continue;
                }

                if (node.Bounds.Intersects(queryArea))
                {
                    results.AddRange(node.Query(queryArea));
                }
            }
            return results;
        }

    }
}
