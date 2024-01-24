using Library_wpf.DB;
using Library_wpf.Models;
using Library_wpf.ViewModelNameSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library_wpf.ViewModel
{
    public class GenreViewModel : BaseViewModel
    {
        private bool addGenreButtonEnabled;
        private Genre selectedGenre;
        private bool deleteGenreButtonEnabled;
        private bool editGenreButtonEnabled;
        private bool saveGenreButtonEnabled;
        private bool isAddGenreButtonClicked;
        private bool isEditGenreButtonClicked;
        private bool isSaveGenreButtonClicked;
        private string genreNameWarningText;
        private bool genreNameTextBoxEnabled;
        private bool clearGenreComboBoxEnabled;
        private RelayCommand addGenreCommand;
        private RelayCommand editGenreCommand;
        private RelayCommand deleteGenreCommand;
        private RelayCommand saveGenreCommand;
        private RelayCommand clearGenreComboBoxCommand;
        private string genreNameText;
        private IGenreDB _genreDB;
        private List<Genre> genreListSource;

        public GenreViewModel(IGenreDB genreDb)
        {
            _genreDB = genreDb;
            _ = GetGenres();
            AddGenreButtonEnabled = true;
        }

        public List<Genre> GenreListSource
        {
            get { return genreListSource; }
            set
            {
                genreListSource = value;
                OnPropertyChanged("GenreListSource");
            }
        }
        public string GenreNameText
        {
            get { return genreNameText; }
            set
            {
                genreNameText = value;
                OnPropertyChanged("GenreNameText");
            }
        }
        public string GenreNameWarningText
        {
            get { return genreNameWarningText; }
            set
            {
                genreNameWarningText = value;
                OnPropertyChanged("GenreNameWarningText");
            }
        }
        public bool AddGenreButtonEnabled
        {
            get { return addGenreButtonEnabled; }
            set
            {
                addGenreButtonEnabled = value;
                OnPropertyChanged("AddGenreButtonEnabled");
            }
        }
        public bool EditGenreButtonEnabled
        {
            get { return editGenreButtonEnabled; }
            set
            {
                editGenreButtonEnabled = value;
                OnPropertyChanged("EditGenreButtonEnabled");
            }
        }
        public bool DeleteGenreButtonEnabled
        {
            get { return deleteGenreButtonEnabled; }
            set
            {
                deleteGenreButtonEnabled = value;
                OnPropertyChanged("DeleteGenreButtonEnabled");
            }
        }
        public bool SaveGenreButtonEnabled
        {
            get { return saveGenreButtonEnabled; }
            set
            {
                saveGenreButtonEnabled = value;
                OnPropertyChanged("SaveGenreButtonEnabled");
            }
        }
        public bool ClearGenreComboBoxEnabled { get { return clearGenreComboBoxEnabled; } set { clearGenreComboBoxEnabled = value; OnPropertyChanged("ClearGenreComboBoxEnabled"); } }
        public bool GenreNameTextBoxEnabled { get { return genreNameTextBoxEnabled; } set { genreNameTextBoxEnabled = value; OnPropertyChanged("GenreNameTextBoxEnabled"); } }
        public Genre SelectedGenre
        {
            get { return selectedGenre; }
            set
            {
                selectedGenre = value;
                if (selectedGenre == GenreListSource[0])
                {
                    AddGenreButtonEnabled = true;
                    EditGenreButtonEnabled = false;
                    DeleteGenreButtonEnabled = false;
                    ClearGenreComboBoxEnabled = false;
                }
                else
                {
                    AddGenreButtonEnabled = false;
                    EditGenreButtonEnabled = true;
                    DeleteGenreButtonEnabled = true;
                    ClearGenreComboBoxEnabled = true;
                }
                OnPropertyChanged("SelectedGenre");
            }
        }
        public RelayCommand SaveGenreCommand { get { return saveGenreCommand ?? (saveGenreCommand = new RelayCommand(obj => SaveGenreCommandMethod())); } }
        public RelayCommand EditGenreCommand { get { return editGenreCommand ?? (editGenreCommand = new RelayCommand(obj => EditGenreCommandMethod())); } }
        public RelayCommand AddGenreCommand { get { return addGenreCommand ?? (addGenreCommand = new RelayCommand(obj => AddGenreCommandMethod())); } }
        public RelayCommand DeleteGenreCommand { get { return deleteGenreCommand ?? (deleteGenreCommand = new RelayCommand(obj => DeleteGenreCommandMethod())); } }
        public RelayCommand ClearGenreComboBoxCommand { get { return clearGenreComboBoxCommand ?? (clearGenreComboBoxCommand = new RelayCommand(obj => ClearGenreComboBoxCommandMethod())); } }
        public async Task GetGenres()
        {
            GenreListSource = await _genreDB.GetGenresAsync();
            ClearGenreComboBoxCommandMethod();
        }
        public int ValidateGenre(Genre genre)
        {
            GenreNameWarningText = string.Empty;
            if (string.IsNullOrEmpty(genre.Name))
            {
                GenreNameWarningText = "*";
                return 2;
            }
            else
            {
                return 1;
            }
        }
        public void ClearGenreText()
        {
            GenreNameText = string.Empty;
            GenreNameWarningText = string.Empty;
        }
        public void AddGenreCommandMethod()
        {
            ClearGenreText();
            GenreNameTextBoxEnabled = true;
            isEditGenreButtonClicked = false;
            isAddGenreButtonClicked = true;
            SaveGenreButtonEnabled = true;
        }
        public async void SaveGenreCommandMethod()
        {
            isSaveGenreButtonClicked = true;
            Genre newGenre = new Genre();
            newGenre.Name = GenreNameText;
            int validateNum = ValidateGenre(newGenre);
            if (validateNum == 1)
            {
                if (isAddGenreButtonClicked)
                {
                    int number = await _genreDB.AddGenre(newGenre);
                    MessageBox.Show($"Added {number} genre.");
                    isAddGenreButtonClicked = false;
                }
                else if (isEditGenreButtonClicked)
                {
                    int number = await _genreDB.EditGenre(SelectedGenre as Genre, newGenre);
                    MessageBox.Show($"Edited {number} genre.");
                    isEditGenreButtonClicked = false;
                }
                _ = GetGenres();
                isSaveGenreButtonClicked = false;
                EditGenreButtonEnabled = false;
                DeleteGenreButtonEnabled = false;
                AddGenreButtonEnabled = true;
                ClearGenreComboBoxCommandMethod();
                ClearGenreText();
            }
            else
            {
                isSaveGenreButtonClicked = false;
            }
        }
        public void EditGenreCommandMethod()
        {
            Genre selectedGenre = SelectedGenre as Genre;
            GenreNameText = selectedGenre.Name;
            isEditGenreButtonClicked = true;
            isAddGenreButtonClicked = false;
            SaveGenreButtonEnabled = true;
            GenreNameTextBoxEnabled = true;
        }
        public async void DeleteGenreCommandMethod()
        {
            if(MessageBox.Show($"Are you sure you want to delete {SelectedGenre.Name} from genres?", "Delete Genre", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
            int result = await _genreDB.DeleteGenre(SelectedGenre);
            MessageBox.Show($"Deleted {result} genre.");
            ClearGenreComboBoxCommandMethod();
            _ = GetGenres();
            }
        }
        public void ClearGenreComboBoxCommandMethod()
        {
            GenreNameTextBoxEnabled = false;
            ClearGenreText();
            SelectedGenre = GenreListSource[0];
        }
    }
}
