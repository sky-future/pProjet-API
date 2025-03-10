﻿using System.Collections.Generic;
using Domain.Cars;

namespace Application.Repositories
{
    public interface ICarRepository
    {
        IEnumerable<ICar> Query();
        ICar GetById(int id);
        ICar Create(ICar car);
        bool DeleteById(int id);
        bool Update(int id, ICar car);
        ICar GetByIdUserCar(int idUser);
        bool PatchPlaceNb(int placeNb, int idUser);
    }
}