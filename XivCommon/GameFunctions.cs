﻿using System;
using Dalamud.Plugin;
using XivCommon.Functions;

namespace XivCommon {
    /// <summary>
    /// A class containing game functions
    /// </summary>
    public class GameFunctions : IDisposable {
        private DalamudPluginInterface Interface { get; }

        /// <summary>
        /// Chat functions
        /// </summary>
        public Chat Chat { get; }

        /// <summary>
        /// Party Finder functions
        /// </summary>
        public PartyFinder PartyFinder { get; }

        /// <summary>
        /// BattleTalk functions and events
        /// </summary>
        public BattleTalk BattleTalk { get; }
        /// <summary>
        /// Examine functions
        /// </summary>
        public Examine Examine { get; }

        /// <summary>
        /// Talk events
        /// </summary>
        public Talk Talk { get; }

        internal GameFunctions(Hooks hooks, DalamudPluginInterface @interface) {
            this.Interface = @interface;

            var scanner = @interface.TargetModuleScanner;
            var seStringManager = @interface.SeStringManager;

            this.Chat = new Chat(this, scanner);
            this.PartyFinder = new PartyFinder(scanner, hooks.HasFlag(Hooks.PartyFinder));
            this.BattleTalk = new BattleTalk(this, scanner, seStringManager, hooks.HasFlag(Hooks.BattleTalk));
            this.Examine = new Examine(this, scanner);
            this.Talk = new Talk(scanner, seStringManager, hooks.HasFlag(Hooks.Talk));
        }

        /// <inheritdoc />
        public void Dispose() {
            this.Talk.Dispose();
            this.BattleTalk.Dispose();
            this.PartyFinder.Dispose();
        }

        /// <summary>
        /// Gets the pointer to the UI module
        /// </summary>
        /// <returns>Pointer</returns>
        public IntPtr GetUiModule() {
            return this.Interface.Framework.Gui.GetUIModule();
        }
    }
}
