using System;

namespace EventBus.Messages.Events
{
    public class IntegrationBusEvent
    {
        public IntegrationBusEvent()
        {
            Id = Guid.NewGuid();
            CreateDate = DateTime.Now;
        }

        public IntegrationBusEvent(Guid id , DateTime createDate)
        {
            Id = id;
            CreateDate = createDate;
        }
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
