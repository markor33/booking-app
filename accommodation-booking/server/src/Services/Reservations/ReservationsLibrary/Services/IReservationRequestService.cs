﻿using ReservationsLibrary.Enums;
using ReservationsLibrary.Models;
using ReservationsLibrary.Utils;

namespace ReservationsLibrary.Services
{
    public interface IReservationRequestService
    {
        public ReservationRequest Create(ReservationRequest request);
        public void ApproveRequest(Guid requestId);
        public void DeclineRequest(Guid requestId);
        public void ChangeStatus(ReservationRequest request, ReservationRequestStatus status);
        public void DeclineOverLapped(DateRange range, Guid accommodationId);
        public List<ReservationRequest> GetByHost(Guid hostId);
        public void DeleteRequest(Guid requestId);
    }
}