import os
import datetime
import cPickle as Pickle
import random


"""
<?php
$fp = fopen("file_name","a");
fwrite($fp,"Data")

?>
"""
all_data = {}

header = """
    <!doctype html>
    <html lang = 'en'>
    <head>
        <title>Fiora tests </title>
        <link href="css/impress-demo.css" rel="stylesheet" />
    </head>
    <body>
    <div id = 'impress'>

      """

divs = """ """

footer = """
    </div>

    <script src = 'js/impress.js'></script>
    <script>impress().init();</script>
    </body>
    </html>
        """


def pickle_loader():
    """
     Loads pervious post data from pickle file.
    """
    global all_data
    f = open('posts.pi', 'r')
    try:
        all_data = Pickle.load(f)
    except EOFError:
        print "Looks like the post data is empty..starting with fresh db"
        pass
    f.close()


def dater():
    """
       returns a list containing the date in isoformat and ctime format.
       isoformat data is used for the div ids and ctime data is used with the post
    """
    t = datetime.datetime.today()
    time = t.ctime().split()
    formatted_datetime = '{0},{1} {2} {3} {4}'.format(
        time[0], time[1], time[2], time[4], time[3])
    return [t.isoformat(), formatted_datetime]


def html_export(name='index', h=header, d=divs, f=footer):
    """
        exports the final html , ready to be hosted.
        Takes 1 params:
            name : Name of the to-be-exported html file.
        3 optional params:
            h : custom header.
            d : custom generated divs.
            f : custom footer .
    """
    if os.path.isfile(name + '.html'):
        os.remove(name + '.html')
    with open(name + '.html', 'wb') as writer:
        writer.write(h + d + f)

def div_generator(dates, post_data):
    """ TODO: let user provide x,y,z and rotaion angle.

        hard coded div generator. Feel free to edit it to meet your needs.
    """
    rotate = [
        'data-rotate-x=\'{0}\'' ,  'data-rotate-y=\'{0}\'' , 'data-rotate-y=\'{0}\'', 'data-rotate=\'{0}\'']
    x = 300  # data-x
    y = 300  # data-y
    z = random.randint(-500, 500)  # data-z
    a = random.randint(0, 180)  # angle
    div_data = "<div id=\'{0}\' class=\'step\' data-x=\'{1}\' data-y=\'{2}\' data-z=\'{3}\' {4}><p><div id= \'date\'>{5}</div>{6}</p></div>\n"
    #return div_data.format(dates[0], x, y, z, rotate[random.randint(0,len(rotate)-1)].format(a), dates[1], post_data)
    return div_data.format(dates[0], x, y, z, random.choice(rotate).format(a), dates[1], post_data) #This one works better :3


def pickle_updater(date,post):
    """
        Updates the all_data dict with the new post.
    """
    global all_data
    all_data[len(all_data) + 1] = [date,post]



def pickle_dumper():
    """
        Dumps the data back to the pickle for future use.
    """
    global all_data
    with open('posts.pi', 'wb') as pickledumper:
        Pickle.dump(all_data, pickledumper)


def div_updater():
    """
       populates the div to write to a fresh HTML.

    """
    global all_data
    global divs
    for i in all_data:
        #divs = all_data[i] + divs
        divs = div_generator( all_data[i][0] , all_data[i][1] ) + divs

p_d = raw_input("What's on your mind? ") #Take post data input. I'll add a gui later on.
#p_d = "something more more more"
pickle_loader()
#pickle_updater( div_generator( dater() , p_d ))
pickle_updater(dater(),p_d)
pickle_dumper()
div_updater()
html_export(name= 'windex',h = header , d = divs , f = footer)


print header + divs + footer


#print all_data
#print dater()
#print div_generator(dater()[0],p_d)
#print divs