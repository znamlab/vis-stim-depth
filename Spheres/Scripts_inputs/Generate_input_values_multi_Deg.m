%% GENERATE INPUT FILES FOR BONSAI SPHERE STIMULATION
%  created @yiranhe
%  edited @yiranhe 20/12/29:
%   1) generate angle values --> convert to spherical XYZ for Sphere018t.bonsai

%%
root = 'E:\vis-stim\vis-stim-depth\Spheres\';
write2folder = 'Files_inputs\';
root = [root,write2folder];

%% Which one to run?
generate_translationXYZ = 1;
generate_ambient_values = 1;
ifSave = 1;

%% GENERATE A SERIES OF LOCATIONS (translation XYZ) FOR MULTIPLE SPHERES TO DISPLAY
if generate_translationXYZ
    
    % Set values 
    tsl.x.n = 10; %how many x positions
    tsl.y.n = 10; %how many y positions
    tsl.z.n = 2; %how many z positions

    tsl.x.filename = 'TranslationX_multi_deg';
    tsl.y.filename = 'TranslationY_multi_deg';
    tsl.z.filename = 'TranslationZ_multi_radius';
    
    tsl.azi.MIN = 45; %azimuth; deg
    tsl.azi.MAX = 135;
    tsl.lat.MIN = -45; %latitude; deg
    tsl.lat.MAX = 45;
    tsl.r.MIN = 3; %radius
    tsl.r.MAX = 5;

    
    % All angle values for calculating spherical coordinates
    tsl.azi.values = linspace(tsl.azi.MIN,tsl.azi.MAX,tsl.x.n); 
    tsl.lat.values = linspace(tsl.lat.MAX,tsl.lat.MIN,tsl.y.n);
    tsl.r.values = linspace(tsl.r.MIN,tsl.r.MAX,tsl.z.n);
    
    
    % Create all combos of xyz in deg or radius
    tsl.xyz.values = [];
    for this_r = tsl.r.values
        for this_azi = tsl.azi.values
            for this_lat = tsl.lat.values        
                this_x = this_azi;
                this_y = this_lat;
                this_z = this_r;
                tsl.xyz.values = [tsl.xyz.values;this_x,this_y,this_z];
            end
        end
    end
    
    
%     % Plot graph to check XYZ, remember Bonsai ZY is Matlab YZ
%     scatter3(tsl.xyz.values(1:tsl.x.n*tsl.y.n,1),tsl.xyz.values(1:tsl.x.n*tsl.y.n,2),tsl.xyz.values(1:tsl.x.n*tsl.y.n,3),'b');
%     hold on;
%     scatter3(tsl.xyz.values((tsl.x.n*tsl.y.n+1):(tsl.x.n*tsl.y.n*2),1),tsl.xyz.values((tsl.x.n*tsl.y.n+1):(tsl.x.n*tsl.y.n*2),2),tsl.xyz.values((tsl.x.n*tsl.y.n+1):(tsl.x.n*tsl.y.n*2),3),'r');
%     hold on;
%     scatter3(tsl.xyz.values((tsl.x.n*tsl.y.n*2+1):(tsl.x.n*tsl.y.n*3),1),tsl.xyz.values((tsl.x.n*tsl.y.n*2+1):(tsl.x.n*tsl.y.n*3),2),tsl.xyz.values((tsl.x.n*tsl.y.n*2+1):(tsl.x.n*tsl.y.n*3),3),'g');
%     xlabel('x');
%     ylabel('y');
%     zlabel('z');


    % Save Translation XYZ to separate csv files 
    if ifSave
        tsl.xyz.T = array2table(tsl.xyz.values);
        writetable(tsl.xyz.T(:,1),[root,tsl.x.filename,'.csv'],'WriteVariableNames',0);
        writetable(tsl.xyz.T(:,2),[root,tsl.y.filename,'.csv'],'WriteVariableNames',0); %Bonsai Y takes Matlab Z values
        writetable(tsl.xyz.T(:,3),[root,tsl.z.filename,'.csv'],'WriteVariableNames',0); %Bonsai Z takes Matlab Y values
    end
    
    disp(['MIN X = ',num2str(min(tsl.xyz.values(:,1)))]);
    disp(['MAX X = ',num2str(max(tsl.xyz.values(:,1)))]);
    disp(['MIN Y = ',num2str(min(tsl.xyz.values(:,2)))]);
    disp(['MAX Y = ',num2str(max(tsl.xyz.values(:,2)))]);
    
end


%% FOR VISUAL STIMULI TO FADE IN
% Generate a series of ambient values
if generate_ambient_values
    
    % Set values 
    FRAMERATE = 100; %Hz
    N_objects = tsl.x.n * tsl.y.n * tsl.z.n ; %num of objects displayed
    INITIAL_DELAY = randi([1 5],1,N_objects); %s
    XLvalue = 1000; %a very large value for total matrix storage 

    ambient.START = 0.664; %for background in DARKGREY
    ambient.END = 1;
    ambient.SUSTAIN = 1;

    ambient.fadein_time = 0.5; %s
    ambient.fadeout_time = 0.5; %s
    ambient.total_time = 4; %S
    ambient.sustain_time = ambient.total_time - ambient.fadein_time - ambient.fadeout_time;
    ambient.filename = 'Ambient_values_multi';


    % Create an "empty" matrix to store ambience values of all objects
    ambient.M = ambient.START * ones(N_objects,XLvalue);

    %Loop through all objects
    for iobj = 1:N_objects

        ambient.delay_time = INITIAL_DELAY(iobj);

        % Generate a series of ambient values
        ambient.initialarr = [ones(1,ambient.delay_time * FRAMERATE) * ambient.START];
        ambient.arr1 = [linspace(ambient.START,ambient.END,(FRAMERATE * ambient.fadein_time))];
        ambient.arr2 = [ones(1,ambient.sustain_time * FRAMERATE+1) * ambient.SUSTAIN];
        ambient.arr3 = [linspace(ambient.END,ambient.START,(FRAMERATE * ambient.fadeout_time))];
        ambient.arr = [ambient.initialarr,ambient.arr1,ambient.arr2,ambient.arr3];

        % Write to total matrix
        ambient.M(iobj,1:size(ambient.arr,2)) = ambient.arr;
        clear ambient.arr;
    end

    % Save to csv
    %ambient.M = ambient.M';  % each trial (displaying all spheres) is a row, each column is a time series of changing ambience of one sphere
    % each trial (displaying the time series of one sphere) is a row
    if ifSave
        ambient.T = array2table(ambient.M);
        writetable(ambient.T,[root,ambient.filename,'.csv'],'WriteVariableNames',0);
    end

end
