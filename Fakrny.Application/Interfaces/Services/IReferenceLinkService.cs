﻿namespace Fakrny.Application.Interfaces.Services;
public interface IReferenceLinkService : IBaseService<ReferenceLink>
{
    IEnumerable<ReferenceLink> GetAllWithDetails();
}
