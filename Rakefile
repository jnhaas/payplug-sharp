task :default => :test

task :clean do
  sh "rm -rf Payplug/bin"
  sh "rm -rf Payplug/obj"
  sh "rm -rf Payplug.Tests/bin"
  sh "rm -rf Payplug.Tests/obj"
end

task :compile => :clean do
  sh "NuGet restore Payplug.sln"
  sh "xbuild Payplug.sln"
end

desc "run tests"
task :test => [:compile] do
  sh "nunit-console Payplug.Tests/bin/Debug/Payplug.Tests.dll"
end
