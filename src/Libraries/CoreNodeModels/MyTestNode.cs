using System;
using System.Collections.Generic;
using DSCore;
using Dynamo.Graph.Nodes;
using ProtoCore.AST.AssociativeAST;

namespace CoreNodeModels
{
    [IsDesignScriptCompatible]
    [NodeName("MyTestNode")]
    [NodeCategory("Core")]
    [InPortNames("inPort")]
    [InPortTypes("int")]
    [OutPortNames("outPort")]
    [OutPortTypes("int")]
    public class MyTestNode : NodeModel
    {
        public MyTestNode()
        {
            RegisterAllPorts();
        }

        public override IEnumerable<AssociativeNode> BuildOutputAst(List<AssociativeNode> inputAstNodes)
        {
            // This won't work.
            var functionCall =
               AstFactory.BuildFunctionCall(new Func<int, int>(I_Want_To_Call_This),
                   new List<AssociativeNode>() { inputAstNodes[0] });

            // But this will work.
            var functionCall2 =
               AstFactory.BuildFunctionCall(new Func<int, int>(DummyClass2.But_Have_To_Call_This),
                   new List<AssociativeNode>() { inputAstNodes[0] });

            return new[]
            {
                AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), /*functionCall*/functionCall2),
            };
        }

        public static int I_Want_To_Call_This(int i)
        {
            return i * 2;
        }
    }
}
