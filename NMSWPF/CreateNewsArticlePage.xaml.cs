using AutoMapper;
using BusinessObject.Model;
using Microsoft.Extensions.DependencyInjection;
using NMSWPF;
using Services.DTO.Request;
using Services.DTO.Response;
using Services.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TrinhLekhoaWPF
{
    /// <summary>
    /// Interaction logic for CreateNewsArticlePage.xaml
    /// </summary>
    public partial class CreateNewsArticlePage : Page
    {
        private readonly INewsArticleServices _newsArticleServices;
        private readonly NewsArticleResponseDTO _newsArticletoUpdate;
        private ObservableCollection<Tag> _selectedTags;
        private readonly ISystemAccountServices _systemAccountServices;
        private readonly ICategoryServices _categoryServices;
        private readonly ManageNewsArticlePage _manageNewsArticlePage;
        private readonly ITagServices _tagServices;
        public CreateNewsArticlePage(INewsArticleServices newsArticleServices, ManageNewsArticlePage manageNewsArticlePage, NewsArticleResponseDTO newsArticletoUpdate = null)
        {
            InitializeComponent();
            _newsArticleServices = newsArticleServices;
            _manageNewsArticlePage = manageNewsArticlePage;
            _newsArticletoUpdate = newsArticletoUpdate;
            _systemAccountServices = ((App)Application.Current).ServiceProvider.GetRequiredService<ISystemAccountServices>();
            _categoryServices = ((App)Application.Current).ServiceProvider.GetRequiredService<ICategoryServices>();
            _tagServices = ((App)Application.Current).ServiceProvider.GetRequiredService<ITagServices>();

            if (_newsArticletoUpdate != null)
            {
                GetDataToUpdate(_newsArticletoUpdate);
            }
            LoadCategoryAndTag();
            _selectedTags ??= new ObservableCollection<Tag>();
            SelectedTagsListBox.ItemsSource = _selectedTags;
        }

        private async void LoadCategoryAndTag()
        {
            await Task.WhenAll(LoadCategory(), LoadTag());
        }

        private async Task LoadCategory()
        {
            try
            {
                var data = await _categoryServices.getAllAsync();
                CategoryComboBox.ItemsSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task LoadTag()
        {
            try
            {
                var data = await _tagServices.getAllAsync();
                TagComboBox.ItemsSource = data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private async void GetDataToUpdate(NewsArticleResponseDTO dto)
        {
            try
            {
                NewsIdTextBox.Text = _newsArticletoUpdate.NewsArticleId.ToString();
                NewsIdTextBox.IsReadOnly = true;
                NewsTitleTextBox.Text = _newsArticletoUpdate.NewsTitle;
                NewsContentTextBox.Text = _newsArticletoUpdate.NewsContent;

                var categoryId = await _categoryServices.getCategoryIdByCaetegoryName(dto.CategoryName);
                CategoryComboBox.SelectedValue = categoryId;
                StatusCheckBox.IsChecked = _newsArticletoUpdate.NewsStatus == "Active";
                CreateByTextBox.Text = _newsArticletoUpdate.CreatedBy;
                _selectedTags = new ObservableCollection<Tag>(_newsArticletoUpdate.Tags);
                SelectedTagsListBox.ItemsSource = _selectedTags;
                ModifiedDateDatePicker.SelectedDate = _newsArticletoUpdate.ModifiedDate;

                CreateButton.Content = "Update";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void RemoveTagButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTagsListBox.SelectedItem != null)
            {
                var selectedTag = (Tag)SelectedTagsListBox.SelectedItem;
                _selectedTags.Remove(selectedTag);
            }
        }

        private void AddTagButton_Click(object sender, RoutedEventArgs e)
        {
            if (TagComboBox.SelectedItem != null)
            {
                var selectTag = (Tag)TagComboBox.SelectedItem;
                if (!_selectedTags.Any(t => t.TagId == selectTag.TagId))
                {
                    _selectedTags.Add(selectTag);
                }

            }
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newId = NewsIdTextBox.Text;
                var newsTitle = NewsTitleTextBox.Text;
                var newsContent = NewsContentTextBox.Text;
                var categoryId = (short)CategoryComboBox.SelectedValue;
                var newsStatus = StatusCheckBox.IsChecked ?? false;
                var createdDate = DateTime.Now;
                var createdBy = (string)CreateByTextBox.Text;
                var tagIds = _selectedTags.Select(t => t.TagId).ToList();
                var Modifield = ModifiedDateDatePicker.SelectedDate ?? DateTime.Now;
                // Create Button
                if (_newsArticletoUpdate == null)
                {

                    if (await _newsArticleServices.getById(newId) != null)
                    {
                        MessageBox.Show("ID is exist!!!");
                    }
                    //var categoryId = await _categoryServices.getCategoryIdByCaetegoryName(categoryName);
                    var CreateById = await _systemAccountServices.getAccountIdByAccountName(createdBy);
                    var newNewsArticle = new NewsArticleRequestDTO
                    {
                        NewsArticleId = newId,
                        NewsTitle = newsTitle,
                        NewsContent = newsContent,
                        CategoryId = categoryId,
                        NewsStatus = newsStatus,
                        CreatedDate = createdDate,
                        CreatedById = CreateById,
                        Tags = tagIds,
                        ModifiedDate = Modifield
                    };
                    var result = await _newsArticleServices.createNewsArticle(newNewsArticle);
                    if (result)
                    {
                        MessageBox.Show("News article created successfully!");
                        await _manageNewsArticlePage.LoadNewsArticle();
                        NavigationService.GoBack();
                    }
                    else
                    {
                        MessageBox.Show("New article is exist!!!");
                    }
                }
                //update Button
                else
                {
                    var updatedto = new NewsArticleRequestDTO
                    {
                        NewsArticleId = _newsArticletoUpdate.NewsArticleId,
                        NewsTitle = newsTitle,
                        NewsContent = newsContent,
                        NewsStatus = newsStatus,
                        CategoryId = categoryId,
                        CreatedDate = createdDate,
                        ModifiedDate = Modifield,
                        CreatedById = await _systemAccountServices.getAccountIdByAccountName(createdBy),
                        Tags = tagIds

                    };
                    await _newsArticleServices.UpdateNewsArticleAsync(updatedto);
                    MessageBox.Show("News article updated successfully!");
                    NavigationService.GoBack();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
