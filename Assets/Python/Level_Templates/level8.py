# https://docs.python.org/3/tutorial/classes.html#a-first-look-at-classes

# In this level, we're going to use a class to help 
# us make cakes.
# You can think of class as like a real-world object.
# Classes have properties and methods.
# Properties are just variables that are tied to the class.
# Methods are functions associated with the class.
# For example, a 'Car' class would have properties like
# 'color', 'number of tires', 'weight', etc.
# and methods like 'drive()', 'honk()', etc.

# We are going to define a CakeFactory class like so:
# class CakeFactory:
#     def __init__(self):
#         self.base = ["Frosting", "Egg", "Sugar", "Flour", "Milk"]

#     def cake(self):
#         return self.base

#     def cake_with_sprinkles(self):
#         return self.base + ["Sprinkles"]

#     def chocolate_cake(self):
#         return ["Chocolate"] + self.base

#     def chocolate_cake_with_sprinkles(self):
#         return ["Chocolate"] + self.base + ["Sprinkles"]

# The CakeFactory class has one property: base.
# base contains the ingredients that are present in all cakes.
# The __init__() method will set the value of base when we 
# create a new CakeFactory.
# The other 4 methods will return the list of ingredients we 
# need for each type of cake.
# So far, we have just defined what a CakeFactory is.
# We need to actually create (a.k.a. instantiate) 
# a CakeFactory.
# Type: factory = CakeFactory()

# Now we have a CakeFactory().
# The __init__() mehtod was already called so we can start 
# using our factory.
# We can call the methods in a loop like so:
# for i in range(3):
#     add_Custom(factory.cake())
#     add_Custom(factory.chocolate_cake())
#     add_Custom(factory.cake_with_sprinkles())
#     add_Custom(factory.chocolate_cake_with_sprinkles())