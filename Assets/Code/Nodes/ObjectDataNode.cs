﻿using Compute.Nodes;
using Compute.Nodes.Builders;
using Compute.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDataNode : ConnectedNode
{
    ObjectType value;

    List<ObjectDataNode> Connections = new List<ObjectDataNode>();

    protected override void Start()
    {
        base.Start();
        spriteRenderer.color = value.TypeColor;
    }


    protected override void OnDragFinish()
    {
        ObjectDataNode hoveredNode = (SelectionManager.Instance.HoverNode as ObjectDataNode);

        if (!hoveredNode)
        {
            Destroy(currentGameObject);
            currentDraggingLine = null;
            return;
        }
        
        if (hoveredNode != this)
        {
            if (hoveredNode.IsInputNode != IsInputNode)
            {
                NodeConnection<ObjectDataNode> nodeConnection = new NodeConnectionBuilder<ObjectDataNode>()
                    .SetNodes(this, hoveredNode)
                    .SetConnectionLine(currentDraggingLine)
                    .Build();

                ConnectedNodes.Add(nodeConnection.Cast<ConnectedNode>());
                hoveredNode.ConnectedNodes.Add(nodeConnection.Cast<ConnectedNode>());

                // todo: register data nodes
                //RegisterExecNodes(nodeConnection);

                nodeConnection.UpdateLine();

                currentGameObject = null;
                return;
            }
        }
        Destroy(currentGameObject);
        currentDraggingLine = null;
    }
}