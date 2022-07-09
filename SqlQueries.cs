internal static class SqlQueries
{
    internal const string GET_PRODUCT_DETAIL = @"
        SELECT
            product.id,
            product.name,
            product.price,
            product.image_url as ImageUrl,
            product.status,
            product.updated_on as UpdatedOn,
            product_metadata.id,
            product_metadata.description,
            product_metadata.specifications
        From
            product
        JOIN product_metadata ON product_metadata.id = product.id";

    internal const string DELETE_PRODUCT = @"
        Delete from product Where id = @Id";

    internal const string DELETE_PRODUCT_METADATA = @"
        Delete from product_metadata Where id = @Id";

    internal const string GET_USER_DETAIL = @"
        SELECT
            user.id,
            user.name,
            user.password_hash as PasswordHash,
            user.password_salt as PasswordSalt,
            user.updated_on
        From
            user";
}