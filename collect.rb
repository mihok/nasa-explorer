require 'net/telnet'
require 'json'

puts "Connecting to HORIZONS..."

horizons = Net::Telnet::new("Host" => "horizons.jpl.nasa.gov", "Timeout" => 20, "Port" => 6775, "Prompt" => />/)

horizons.waitfor(/Horizons>/) 

puts "Connected to HORIZONS!"
puts "Requesting for Major Bodies list..."

horizons.puts("M")
list = ""
horizons.waitfor(/<cr>:/) do |msg|
  list << msg
end
horizons.puts("")

bodies = Hash.new
list.scan(/^ *(-?[0-9]{1,})  ([a-zA-Z\-0-9]* [a-zA-Z\-0-9]* [a-zA-Z\-0-9]*)/) do |match|
  bodies[match[1].downcase.rstrip] = {:id => match[0]} unless (match[1] =~ /[a-zA-Z]/).nil?
end

File.open('bodies.json', 'w') { |f| f.write bodies.to_json }

puts "Received and parsed Major Bodies list. #{bodies.length} bodies found."

horizons.waitfor(/Horizons>/)
total = bodies.length
i = 0
bodies.each do |name, body|
  i += 1
  puts "Requesting data for planet #{name} (#{i}/#{total})"
  horizons.puts(body[:id])
  data = ""
  horizons.waitfor(/<cr>:/) do |msg|
    data << msg
  end
  horizons.puts("")
  horizons.waitfor(/Horizons>/)

  #Add more regular expressions below
  regexes = {:mass => /Mass, \(?(?<unit>.*kg)\)? *= (?<value>[0-9.])*/, :atm_pressure => /Atm. pressure *= (?<value>[0-9.]*) (?<unit>bar)/, :gravity => /ge, (?<unit>.*)\(equatorial\) *= (?<value>[0-9.]*)/}

  regexes.each do |attr, regex|
    match = regex.match(data)
    unless match.nil?
      bodies[name][attr] = "#{match["value"]} #{match["unit"]}" 
    end
  end
end

File.open('bodies.json', 'w') { |f| f.write bodies.to_json }

# [a-zA-Z][a-zA-Z,.H0-9\^\-\(\)/' ]*= [0-9.+-/*-\^]*( deg)?( *(x|km))?( [0-9]*\^[0-9]*)?( *(kg|km|yrs|days|gauss|bar))?