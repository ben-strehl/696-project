class OperationList:
    def __init__(self):
        self.text = []

    def append(self, ingredient: str):
        self.text.append(ingredient)

    def get_self(self):
        return self

opList = OperationList()

def add_Flour():
    opList.append('{ "type": 0, "name": "Flour" }')

def add_Sugar():
    opList.append('{ "type": 1, "name": "Sugar" }')

def add_Milk():
    opList.append('{ "type": 2, "name": "Milk" }')

def add_Egg():
    opList.append('{ "type": 3, "name": "Egg" }')

def add_Frosting():
    opList.append('{ "type": 4, "name": "Frosting" }')

def add_Chocolate():
    opList.append('{ "type": 5, "name": "Chocolate" }')

def add_Vanilla():
    opList.append('{ "type": 6, "name": "Vanilla" }')

def add_Sprinkles():
    opList.append('{ "type": 7, "name": "Sprinkles" }')

def add_Cake():
    opList.append('{ "type": 8, "name": "Cake" }')
