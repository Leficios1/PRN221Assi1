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
        public CreateNewsArticlePage(INewsArticleServices newsArticleServices, ManageNewsArticlePage manageNewsArticlePage, NewsArticleResponseDTO newsArticle = null)
        {
            InitializeComponent();
            _newsArticleServices = newsArticleServices;
            _newsArticletoUpdate = newsArticle;
            _systemAccountServices = ((App)Application.Current).ServiceProvider.GetRequiredService<ISystemAccountServices>();
            _categoryServices = ((App)Application.Current).ServiceProvider.GetRequiredService<ICategoryServices>();
            _manageNewsArticlePage = manageNewsArticlePage;

            LoadCategory();

            if (_newsArticletoUpdate != null)
            {
                NewsTitleTextBox.Text = _newsArticletoUpdate.NewsTitle;
                NewsContentTextBox.Text = _newsArticletoUpdate.NewsContent;
                CategoryComboBox.SelectedValue = _newsArticletoUpdate.CategoryName;
                StatusCheckBox.IsChecked = _newsArticletoUpdate.NewsStatus == "Active";
                CreateByTextBox.Text = _newsArticletoUpdate.CreatedBy;
                _selectedTags = new ObservableCollection<Tag>(_newsArticletoUpdate.Tags);
                ModifiedDateDatePicker.SelectedDate = _newsArticletoUpdate.ModifiedDate;

                CreateButton.Content = "Update";
            }
            SelectedTagsListBox.ItemsSource = _selectedTags;
        }

        private async void LoadCategory()
        {
            try
            {
                var data = await _newsArticleServices.GetAllCategories();
                CategoryComboBox.ItemsSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private async void LoadCreateByName(short? id)
        //{
        //    try
        //    {
        //        var data = await _newsArticleServices.getCreateNameByCreateId(id);
        //        if (data != null)
        //        {
        //            CreateByTextBox.Text = data;
        //        }
        //        else
        //        {
        //            MessageBox.Show("Not Found ID");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

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
            if(TagComboBox.SelectedItem != null)
            {
                var selectTag = (Tag)TagComboBox.SelectedItem;
                if (!_selectedTags.Contains(selectTag))
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
                var categoryName = (short)CategoryComboBox.SelectedValue;
                var newsStatus = StatusCheckBox.IsChecked ?? false;
                var createdDate = DateTime.Now;
                var createdBy = (string)CreateByTextBox.Text;
                var tagIds = SelectedTagsListBox.Items.Cast<Tag>().Select(t => t.TagId).ToList();
                var Modifield = ModifiedDateDatePicker.SelectedDate ?? DateTime.Now;

                if(await _newsArticleServices.getById(newId) != null)
                {
                    MessageBox.Show("LMBV");
                }
                if (_newsArticletoUpdate == null)
                {
                    //var categoryId = await _categoryServices.getCategoryIdByCaetegoryName(categoryName);
                    var CreateById = await _systemAccountServices.getAccountIdByAccountName(createdBy);
                    var newNewsArticle = new NewsArticleRequestDTO
                    {
                        NewsArticleId = newId,
                        NewsTitle = newsTitle,
                        NewsContent = newsContent,
                        CategoryId = categoryName,
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
                else
                {
                    var updatedto = new NewsArticleRequestDTO
                    {
                        NewsArticleId = _newsArticletoUpdate.NewsArticleId,
                        NewsTitle = newsTitle,
                        NewsContent = newsContent,
                        NewsStatus = newsStatus,
                        CreatedDate = createdDate,
                        ModifiedDate = Modifield,
                        CreatedById = await _systemAccountServices.getAccountIdByAccountName(createdBy),
                        Tags = tagIds

                    };
                    await _newsArticleServices.UpdateNewsArticleAsync(updatedto);
                    MessageBox.Show("News article updated successfully!");
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
