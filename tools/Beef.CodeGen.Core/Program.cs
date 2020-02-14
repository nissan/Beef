﻿// Copyright (c) Avanade. Licensed under the MIT License. See https://github.com/Avanade/Beef

using System.Threading.Tasks;

namespace Beef.CodeGen
{
    /// <summary>
    /// Provides the console capabilities.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point.
        /// </summary>
        /// <param name="args">The console arguments.</param>
        /// <returns>A statuc code.</returns>
        public static Task<int> Main(string[] args)
        {
            return CodeGenConsole.Create().RunAsync(args);
        }
    }
}
