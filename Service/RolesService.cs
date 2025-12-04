using pract12.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace pract12.Service
{
    public class RolesService
    {
        private readonly AppDbContext _db = BaseDbService.Instance.Context;
        public static ObservableCollection<Role> Roles { get; set; } = new();

        public void GetAll()
        {
            var roles = _db.Roles.OrderBy(r => r.Id).ToList();
            Roles.Clear();
            foreach (var role in roles)
                Roles.Add(role);
        }

        public int Commit() => _db.SaveChanges();

        public RolesService()
        {
            GetAll();
        }

        public Role GetRoleById(int id)
        {
            return _db.Roles.FirstOrDefault(r => r.Id == id);
        }

        public Role GetRoleByTitle(string title)
        {
            return _db.Roles.FirstOrDefault(r => r.Title == title);
        }

        public void Add(Role role)
        {
            var _role = new Role
            {
                Title = role.Title,
            };
            _db.Add<Role>(_role);
            Commit();
            Roles.Add(_role);
        }

        public void Remove(Role role)
        {

            if (role.Id <= 3)
            {
                throw new InvalidOperationException("Нельзя удалить стандартные роли (Пользователь, Менеджер, Администратор)");
            }

            _db.Remove<Role>(role);
            if (Commit() > 0)
            {
                var roleToRemove = Roles.FirstOrDefault(r => r.Id == role.Id);
                if (roleToRemove != null)
                    Roles.Remove(roleToRemove);
            }
        }

        public void EnsureRolesExist()
        {
            if (!_db.Roles.Any())
            {
                var defaultRoles = new List<Role>
                {
                    new Role { Title = "Пользователь" },
                    new Role { Title = "Менеджер" },
                    new Role { Title = "Администратор" }
                };

                _db.Roles.AddRange(defaultRoles);
                _db.SaveChanges();
                GetAll();
            }
        }

        public void LoadRelation(Role role, string relation)
        {
            var entry = _db.Entry(role);
            var navigation = entry.Metadata.FindNavigation(relation)
                ?? throw new InvalidOperationException($"Navigation '{relation}' not found");

            if (navigation.IsCollection)
            {
                entry.Collection(relation).Load();
            }
            else
            {
                entry.Reference(relation).Load();
            }
        }
    }
}