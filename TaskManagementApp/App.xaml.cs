using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Data;
using TaskManagementApp.Models;
using TaskManagementApp.Services;
using TaskManagementApp.Stores;
using TaskManagementApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace TaskManagementApp
{
    public partial class App : Application
    {
        private const string CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = TaskManagementAppDb;";

        private readonly AppDbContextFactory _appDbContextFactory;
        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        private readonly ISessionService _sessionService;
        private readonly IUserService _userService;
        private readonly ITaskService _taskService;
        private readonly INavigationService _closeModalNavigationService;

        public App(){
            _appDbContextFactory = new AppDbContextFactory(CONNECTION_STRING);
            _navigationStore = new NavigationStore();
            _modalNavigationStore = new ModalNavigationStore();

            _userService = new UserService(_appDbContextFactory);
            _taskService = new TaskService(_appDbContextFactory);
            _sessionService = new SessionService();
            _closeModalNavigationService = new CloseModalNavigationService(_modalNavigationStore);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");

            using (AppDbContext dbContext = _appDbContextFactory.CreateDbContext()) {
                dbContext.Database.Migrate();
            }

            System.Threading.Tasks.Task.Run(async () => await PopulateDatabaseAsync()).Wait();

            _navigationStore.CurrentViewModel = CreateLoginViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore, _modalNavigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private LoginViewModel CreateLoginViewModel()
        {
            return new LoginViewModel( _userService,
                                       _sessionService,
                                       new NavigationService(_navigationStore, CreateRegisterViewModel),
                                       new NavigationService(_navigationStore, CreateTasksMenuViewModel));
        }

        private RegisterViewModel CreateRegisterViewModel()
        {
            return new RegisterViewModel( _userService,
                                          new NavigationService(_navigationStore, CreateLoginViewModel));
        }

        private TasksMenuViewModel CreateTasksMenuViewModel()
        {
            return TasksMenuViewModel.LoadTasksMenuViewModel( _taskService,
                                                              _sessionService,
                                                              new NavigationService(_navigationStore, CreateLoginViewModel),
                                                              new ModalNavigationService(_modalNavigationStore, CreateCreateTaskViewModel),
                                                              new ModalNavigationService(_modalNavigationStore, CreateUpdateTaskViewModel));
        }

        private CreateTaskViewModel CreateCreateTaskViewModel()
        {

            return new CreateTaskViewModel( _taskService,
                                            _sessionService,
                                            _closeModalNavigationService);
        }

        private UpdateTaskViewModel CreateUpdateTaskViewModel()
        {

            return new UpdateTaskViewModel(_taskService,
                                           _closeModalNavigationService);
        }


        private async System.Threading.Tasks.Task PopulateDatabaseAsync()
        {
            using (AppDbContext dbContext = _appDbContextFactory.CreateDbContext()) {
                if (await dbContext.Tasks.AnyAsync()) return;

                Guid user1Id = Guid.NewGuid();
                Guid user2Id = Guid.NewGuid();

                var users = new List<User> {
                    new User {
                        Id = user1Id,
                        Username = "Test1",
                        Email = "Test1",
                        Password = PasswordHash.CreateHash("qwerty"),
                    },
                     new User {
                        Id = user2Id,
                        Username = "Test2",
                        Email = "Test2",
                        Password = PasswordHash.CreateHash("qwerty"),
                    }
                };

                var tasks = new List<Models.Task>{
                    new Models.Task{
                        Id = Guid.NewGuid(),
                        Name = "Ukończyć tą aplikację",
                        Description = "Utworzyć aplikację do zarządzania listą zadań. Aplikacja powinna umożliwiać dodawanie, edytowanie, usuwanie i przeglądanie zadań. Dodatkowo, zadania powinny być przechowywane w bazie danych SQL. ",
                        Deadline = new DateTime(2023, 10, 26),
                        Status = Models.TaskStatus.InProgress,
                        Priority = 999,
                        IsHidden = false,
                        UserId = user1Id
                    },
                    new Models.Task{
                        Id = Guid.NewGuid(),
                        Name = "Dokupić akcje Ehang`a",
                        Description = "Brać poniżej 14,5$ przed sprawozdaniem finansowym",
                        Deadline = new DateTime(2023, 11, 30),
                        Priority = 5,
                        IsHidden = false,
                        UserId = user1Id
                    },
                    new Models.Task{
                        Id = Guid.NewGuid(),
                        Name = "Kupić ostrzałke do łańcucha",
                        Description = "",
                        Deadline = null,
                        Priority = 4,
                        IsHidden = false,
                        UserId = user1Id
                    },
                    new Models.Task{
                        Id = Guid.NewGuid(),
                        Name = "Dodać nowych przeciwników do gry Slasher",
                        Description = "1. Przeciwnik skaczący w kierunku bohatera co pewną stałą sekwencję czasu. \n2. ...",
                        Deadline = new DateTime(2023, 11, 1),
                        Priority = 3,
                        IsHidden = false,
                        UserId = user1Id
                    },
                    new Models.Task{
                        Id = Guid.NewGuid(),
                        Name = "Pociąć gałęzie",
                        Description = "",
                        Priority = 10,
                        IsHidden = false,
                        UserId = user1Id
                    },
                    new Models.Task{
                        Id = Guid.NewGuid(),
                        Name = "Wyciąć pień drzewa (po zakupie ostrzałki)",
                        Description = "",
                        Priority = 10,
                        IsHidden = false,
                        UserId = user1Id
                    },
                };

                dbContext.AddRange(users);
                dbContext.AddRange(tasks);

                dbContext.SaveChanges();
            }
        }
    }
}
