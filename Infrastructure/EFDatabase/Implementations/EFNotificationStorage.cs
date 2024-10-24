using Domain.Model;
using Domain.Model.Notification;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EFDatabase.Implementations
{
    public class EFNotificationStorage : INotificationStorageRepository
    {
        public Task<IReadOnlyCollection<NotificationStored>> GetSortedByDate(UserIdentifier userId)
        {
            throw new NotImplementedException();
        }

        public Task Store(NotificationStored notification)
        {
            throw new NotImplementedException();
        }
    }
}
