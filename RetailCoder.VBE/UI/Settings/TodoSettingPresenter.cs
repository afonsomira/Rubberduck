﻿using System;
using System.ComponentModel;
using System.Linq;
using Rubberduck.Settings;

namespace Rubberduck.UI.Settings
{
    public class TodoSettingPresenter
    {
        private readonly ITodoSettingsView _view;
        private readonly IAddTodoMarkerView _addTodoMarkerView;

        public ToDoMarker ActiveMarker
        {
            get { return _view.TodoMarkers[_view.SelectedIndex]; }
        }

        public TodoSettingPresenter(ITodoSettingsView view, IAddTodoMarkerView addTodoMarkerView)
        {
            _view = view;
            _addTodoMarkerView = addTodoMarkerView;

            _view.AddMarker += AddMarker;
            _view.RemoveMarker += RemoveMarker;
            _view.PriorityChanged += SaveMarker;

            _addTodoMarkerView.AddMarker += ConfirmAddMarker;
            _addTodoMarkerView.Cancel += CancelAddMarker;
        }

        ~TodoSettingPresenter()
        {
            _addTodoMarkerView.Close();
        }

        private void SaveMarker(object sender, EventArgs e)
        {
            //todo: add test; How? I can't click the save button. Code smell here.
            var index = _view.SelectedIndex;
            _view.TodoMarkers[index].Priority = _view.ActiveMarkerPriority;
        }

        private void RemoveMarker(object sender, EventArgs e)
        {
            var oldList = _view.TodoMarkers.ToList();
            oldList.RemoveAt(_view.SelectedIndex);
            _view.TodoMarkers = new BindingList<ToDoMarker>(oldList);
        }

        private void AddMarker(object sender, EventArgs e)
        {
            _addTodoMarkerView.TodoMarkers = _view.TodoMarkers.ToList();
            _addTodoMarkerView.Show();
        }

        private void ConfirmAddMarker(object sender, EventArgs e)
        {
            _addTodoMarkerView.Hide();
            _view.TodoMarkers = new BindingList<ToDoMarker>(_addTodoMarkerView.TodoMarkers);
        }

        private void CancelAddMarker(object sender, EventArgs e)
        {
            _addTodoMarkerView.Hide();
        }

        public void SetActiveItem(int index)
        {
            _view.SelectedIndex = index;
        }
    }
}
