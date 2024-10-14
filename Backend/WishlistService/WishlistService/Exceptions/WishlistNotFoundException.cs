namespace WishlistService.Exceptions
{
    public class WishlistNotFoundException:Exception
    {
        public WishlistNotFoundException() : base("The requested wishlist item was not found.")
        {
        }

        public WishlistNotFoundException(string message) : base(message)
        {
        }

        public WishlistNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
