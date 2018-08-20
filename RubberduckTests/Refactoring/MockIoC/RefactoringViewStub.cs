﻿using System;
using Rubberduck.Refactorings;

namespace RubberduckTests.Refactoring.MockIoC
{
    internal abstract class RefactoringViewStub<TModel> : IRefactoringView<TModel>
        where TModel : class
    {
        protected RefactoringViewStub(TModel model)
        {
            Model = model;
        }

        public virtual object DataContext { get; set; }
        public virtual TModel Model { get; set; }
    }
    
    internal abstract class RefactoringViewModelStub<TModel> : IRefactoringViewModel<TModel>
    {
        protected RefactoringViewModelStub(TModel model)
        {
            Model = model;
        }

        public event EventHandler<RefactoringDialogResult> OnWindowClosed;
        public TModel Model { get; }
    }
}
