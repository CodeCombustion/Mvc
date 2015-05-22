﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Razor.Generator;
using Microsoft.AspNet.Razor.Generator.Compiler;
using Microsoft.AspNet.Razor.Generator.Compiler.CSharp;
using Microsoft.Framework.Internal;

namespace Microsoft.AspNet.Mvc.Razor
{
    public class Utf8LiteralCSharpCodeVisitor : CSharpCodeVisitor
    {
        private readonly CSharpCodeVisitor _parent;

        public Utf8LiteralCSharpCodeVisitor(
            CSharpCodeVisitor parent,
            [NotNull] CSharpCodeWriter writer,
            [NotNull] CodeBuilderContext context)
            : base(writer, context)
        {
            _parent = parent;
        }

        public override void Accept([NotNull]Chunk chunk)
        {
            if (chunk is Utf8Chunk)
            {
                Visit((Utf8Chunk)chunk);
            }
            else
            {
                _parent.Accept(chunk);
            }
        }

        protected virtual void Visit(Utf8Chunk chunk)
        {
            Writer.Write($"new byte[{chunk.Bytes.Length}]");
            Writer.Write(" { ");
            for (int i = 0; i < chunk.Bytes.Length; i++)
            {
                Writer.Write(chunk.Bytes[i].ToString());
                Writer.Write(", ");
            }
            Writer.Write(" }");
        }
    }
}