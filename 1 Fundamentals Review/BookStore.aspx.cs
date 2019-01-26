using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookStore : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            ////get all books in the catalog.
            List<Book> books = BookCatalogDataAccess.GetAllBooks();
            foreach (Book book in books)
          
            ////todo: Populate dropdown list selections
            
            {
                ListItem item = new ListItem(book.Title, book.Id);
                drpBookSelection.Items.Add(item);
            }
        }
       

        ShoppingCart cart = null;
        if (Session["shoppingcart"] == null)
        {

            cart = new ShoppingCart();
            ////todo: add cart to the session YES

            Session["shoppingcart"] = cart;

        }
        else
        {
            ////todo: retrieve cart from the session YES
            
            cart = (ShoppingCart)Session["shoppingcart"];


            foreach (BookOrder order in cart.BookOrders)
            {
                //todo: Remove the book in the order from the dropdown list

                // Removing it here seems redundant when I remove when after adding them 
                //to my cart in the btnAddToCart_Click() method?

            }
        }
        if (cart.NumOfItems == 0)
        {
            lblNumItems.Text = "empty";
        }


    }
    protected void drpBookSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpBookSelection.SelectedValue != "-1")
        {
            string bookId = drpBookSelection.SelectedItem.Value;
            Book selectedBook = BookCatalogDataAccess.GetBookById(bookId);

            ////todo: Add selected book to the session  
            ///Why do I add the selected book to the session when the index changes and not when added to cart?


            //todo: Display the selected book's description and price  
            lblPrice.Text = String.Format("{0:C}", selectedBook.Price);
            lblDescription.Text = selectedBook.Description;
        }
        else
        {
            //todo: Set description and price to blank YES
            lblPrice.Text = "";
            lblDescription.Text = "";
        }
    }
    protected void btnAddToCart_Click(object sender, EventArgs e)
    {
        if (drpBookSelection.SelectedValue != "-1" && Session["shoppingcart"] != null)
        {
            //////todo: Retrieve selected book from the session
            
            var book = BookCatalogDataAccess.GetBookById(drpBookSelection.SelectedValue);

            ////todo: get user entered quqntity
            int quantity = Int16.Parse(txtQuantity.Text);

            ////todo: Create a book order with selected book and quantity

            BookOrder order = new BookOrder(book, quantity);

            ////todo: Retrieve to cart from the session

            var cart = (ShoppingCart)Session["shoppingcart"];

            ////todo: Add book order to the shopping cart

            cart.AddBookOrder(order);

            ////todo: Remove the selected item from the dropdown list
            drpBookSelection.Items.RemoveAt(drpBookSelection.SelectedIndex);
            
            ////todo: Set the dropdown list's selected value as "-1"
            drpBookSelection.ClearSelection();
            txtQuantity.Text = string.Empty;

            ////todo: Set the description to show title and quantity of the book user added to the shopping cart

            lblDescription.Text = "You have added " + quantity + " copies of: " + book.Title;

            ////todo: Update the number of items in shopping cart displayed next to the link to ShoppingCartView.aspx

            if (cart.NumOfItems == 0)
                
                lblNumItems.Text = "empty";
            else if (cart.NumOfItems == 1)
                lblNumItems.Text = "1 item";
            else
                lblNumItems.Text = cart.NumOfItems.ToString() + " items";
        }
    }
}