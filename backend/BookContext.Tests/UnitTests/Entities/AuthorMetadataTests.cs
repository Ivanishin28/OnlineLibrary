using BookContext.Domain.DomainEvents;
using BookContext.Domain.Entities;
using BookContext.Domain.ValueObjects;
using Shared.Core.Extensions;

namespace BookContext.Tests.UnitTests.Entities;

internal class AuthorMetadataTests
{
    [Test]
    public void Set_avatar_without_avatar_should_raise_avatar_set_event()
    {
        var id = new AuthorId(Guid.NewGuid());
        var avatarId = new MediaFileId(Guid.NewGuid());
        var sut = AuthorMetadata
            .Create(id, TimeExtensions.Today())
            .Model;

        sut.SetAvatar(avatarId);

        var events = sut
            .DomainEvents
            .OfType<AuthorAvatarSetDomainEvent>();
        Assert.That(events.Count(), Is.EqualTo(1));
        Assert.That(
            events
                .Any(x => 
                    x.AuthorId == id && 
                    x.AvatarId == avatarId), 
            Is.True);
    }

    [Test]
    public void Removing_avatar_should_raise_avatar_removed_event()
    {
        var id = new AuthorId(Guid.NewGuid());
        var avatarId = new MediaFileId(Guid.NewGuid());
        var sut = AuthorMetadata
            .Create(id, TimeExtensions.Today())
            .Model;
        sut.SetAvatar(avatarId);

        sut.SetAvatar(null);

        var events = sut.DomainEvents.OfType<AuthorAvatarRemovedDomainEvent>();
        Assert.That(events.Count(), Is.EqualTo(1));
        Assert.That(
            events
                .Any(x =>
                    x.AuthorId == id &&
                    x.AvatarId == avatarId),
            Is.True);
    }
}
