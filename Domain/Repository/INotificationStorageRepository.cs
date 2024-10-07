using Domain.Model.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface INotificationStorageRepository
    {
        Task Store(Notification notification);
        Task<List<Notification>> GetSortedByDate(ulong userId);
    }
}
