﻿namespace Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.BuyerAggregate
{
    using SeedWork;

    /// <remarks>
    ///     Card type class should be marked as abstract with protected constructor to encapsulate known enum types
    ///     this is currently not possible as OrderingContextSeed uses this constructor to load cardTypes from csv file
    /// </remarks>
    public class CardType
        : Enumeration
    {
        public static CardType Amex = new CardType(1, nameof(Amex));
        public static CardType Visa = new CardType(2, nameof(Visa));
        public static CardType MasterCard = new CardType(3, nameof(MasterCard));

        public CardType(int id, string name)
            : base(id, name) { }
    }
}
