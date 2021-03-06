﻿namespace Kronos
{
    using System;

    internal class Presenter<TView> where TView : class, IView
    {
        public Presenter(TView view)
        {
            this.View = view ?? throw new ArgumentNullException(nameof(view));
            this.View.Initialize += this.OnViewInitialize;
            this.View.Load += this.OnViewLoad;
        }

        public TView View { get; set; }

        protected virtual void OnViewInitialize(object sender, EventArgs e) 
        { 
        }

        protected virtual void OnViewLoad(object sender, EventArgs e) 
        {
        }
    }
}