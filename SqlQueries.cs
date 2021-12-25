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
        JOIN product_metadata ON product_metadata.id = product.id
        WHERE product.id = @Id";
}