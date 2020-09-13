using System.Collections.ObjectModel;

namespace Take.Be.Domain
{
    public class User : Entity<int>
    {
        public override int Id => base.Id;

        public string NickName { get; private set; }

        public ObservableCollection<Message> Messages { get; private set; }
    }
}
